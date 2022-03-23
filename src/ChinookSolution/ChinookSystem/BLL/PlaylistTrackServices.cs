﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
#endregion


namespace ChinookSystem.BLL
{
    public class PlaylistTrackServices
    {
        #region Constructor and Context Dependency
        private readonly ChinookContext _context;

        //obtain the context link from IServiceCollection when this
        //  set of service is injected into the "outside user"
        internal PlaylistTrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<PlaylistTrackInfo> PlaylistTrack_FetchTracks(string playlistname,
                                                                            string username)
        {
            IEnumerable<PlaylistTrackInfo> info = _context.PlaylistTracks
                                                    .Where(x => x.Playlist.Name.Equals(playlistname)
                                                            && x.Playlist.UserName.Equals(username))
                                                    .Select(x => new PlaylistTrackInfo
                                                    {
                                                        TrackId = x.TrackId,
                                                        TrackNumber = x.TrackNumber,
                                                        SongName = x.Track.Name,
                                                        Milliseconds = x.Track.Milliseconds
                                                    })
                                                    .OrderBy(x => x.TrackNumber);
            return info.ToList();
        }
        #endregion

        #region Commands
        public void PlaylistTrack_AddTrack(string playlistname, string username, int trackid)
        {
            //create local variables
            Track trackExists = null;
            Playlist playlistExists = null;
            PlaylistTrack playlisttrackExists = null;
            int tracknumber = 0;

            //create a LIst<Exception> to contain all discovered errors
            List<Exception> errorlist = new List<Exception>();

            //Business logic
            //these are processing rules that need to be satisfied for valid data
            //  rule: a track can only exist once on a playlist
            //  rule: each track on a playlist is assigned a continuous track number
            //
            //if the business rules are passed, consider the data valid, then
            //  a) stage your transaction work (Adds, Updates, Deletes)
            //  b) execute a SINGLE .SaveChanges() - commits to database

            //parameter validation
            if (string.IsNullOrWhiteSpace(playlistname))
            {
                throw new ArgumentNullException("Playlist name is missing");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("User name is missing");
            }

            trackExists = _context.Tracks
                        .Where(x => x.TrackId == trackid)
                        .FirstOrDefault();
            if (trackExists == null)
            {
                errorlist.Add(new Exception("Selected track no longer is on file. Refresh track table."));
            }

            //business process
            playlistExists = _context.Playlists
                            .Where(x => x.Name.Equals(playlistname)
                                    && x.UserName.Equals(username))
                            .FirstOrDefault();

            if (playlistExists == null)
            {
                //new playlist
                playlistExists = new Playlist()
                {
                    Name = playlistname,
                    UserName = username
                };
                //stage (only in memory)
                _context.Playlists.Add(playlistExists);
                tracknumber = 1;
            }
            else
            {
                //playlist already exists
                //rule: unique tracks on playlist
                playlisttrackExists = _context.PlaylistTracks
                                .Where(x => x.Playlist.Name.Equals(playlistname)
                                        && x.Playlist.UserName.Equals(username)
                                        && x.TrackId == trackid)
                                .FirstOrDefault();
                if(playlisttrackExists != null)
                {
                    var songname = _context.Tracks
                                    .Where(x => x.TrackId == trackid)
                                    .Select(x => x.Name)
                                    .SingleOrDefault();
                    //rule violation
                    errorlist.Add(new Exception($"Selected track ({songname}) is already on the playlist."));

                }
                else
                {
                    tracknumber = _context.PlaylistTracks
                            .Where(x => x.Playlist.Name.Equals(playlistname)
                                    && x.Playlist.UserName.Equals(username))
                            .Count();
                    tracknumber++;
                }
            }

            //add the track to the playlist
            //create an instance for the playlist track
            playlisttrackExists = new PlaylistTrack();

            //load values
            playlisttrackExists.TrackId = trackid;
            playlisttrackExists.TrackNumber = tracknumber;

            //?? what about the second part of the primary key: PlayListID?
            //if the playlist exists then we know the id: playlistExists.PlaylistID
            //BUT if the playlist is NEW, we DO NOT know the id

            //in the situation of a NEW playlist, even though we have
            //  created the playlist instance (see above) it is ONLY
            //  staged!!!!
            //this means that the actual sql record has NOT yet been created
            //this means that the IDENTITY value for the new playlist DOES NOT
            //  yet exist. The value on the playlist instance (playlistExist)
            //  is zero (0).
            //therefore we have a serious problem

            //Solution
            //It is built into the EntityFramework software and is based using the
            //  navigational property in PlayList pointing to its's "child"

            //staging a typical Add in the past was to reference the entity
            //  and use the entity.Add(xxxx)
            //      _context.PlaylistTrack.Add(playlisttrackExists)
            //IF you use this statement the playlistid would be zero (0)
            //  causing your transaction to ABORT
            //Why?? pKeys cannote be zero(0) (FKey to PKey problem)

            //INSTEAD, do the staging using the "parent.navchildproperty.Add(xxxx)
            playlistExists.PlaylistTracks.Add(playlisttrackExists);

            //Staging is complete
            //Commit the work (Transaction)
            //commiting the work needs a .SaveChanges()
            //a transaction has ONLY ONE .SaveChanges
            //BUT what if you have discovered errors during the business process???
            //  if so, then throw all errors and DO NOT COMMIT!!!!
            if (errorlist.Count > 0)
            {
                //throw the list of business processing error(s)
                throw new AggregateException("Unable to add new track. Check concerns", errorlist);
            }
            else
            {
                //consider data valid
                //has passed business processing rules
                _context.SaveChanges();
            }


        }
        #endregion
    }
}
