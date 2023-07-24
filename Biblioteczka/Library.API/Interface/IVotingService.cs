using Library.API.Models;

namespace Library.API.Interface
{
    public interface IVotingService
    {
        public bool Vote(VoteUserDto user);
        public bool RestartVoting();
    }
}
