using BasicAuth.DAL.Repo.Abstract;
using System.ComponentModel;

namespace BasicAuth.Service.Abstract
{
    public interface IAppStateService
    {
        bool IsBusy { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
        IAuthService GetAuthService();
        IUserRepo GetUserRepo();
        void LockPortaitOrientation(object sender, DisplayInfoChangedEventArgs e);
    }
}