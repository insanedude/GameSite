using System.Threading.Tasks;
using GameSiteProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GameSiteProject.Controllers;

public class BaseController : Controller
{
    private readonly IStringLocalizer<HomeController> _localizer;
    private readonly UserManager<User> _userManager;

    public BaseController(IStringLocalizer<HomeController> localizer, UserManager<User> userManager)
    {
        _localizer = localizer;
        _userManager = userManager;
    }
    
    public async Task SetNicknameAsync()
    {
        ViewData["Register"] = _localizer["Register"];
        ViewData["Login"] = _localizer["Login"];
        ViewData["CreateDiscussion"] = _localizer["CreateDiscussion"];
        ViewData["Filter"] = _localizer["Filter"];
        ViewData["GameName"] = _localizer["GameName"];
        ViewData["CreatedBy"] = _localizer["CreatedBy"];
        ViewData["PostedOn"] = _localizer["PostedOn"];
        ViewData["DiscussionDescr"] = _localizer["DiscussionDescr"];
        ViewData["SortByDate"] = _localizer["SortByDate"];
        ViewData["Descending"] = _localizer["Descending"];
        ViewData["Ascending"] = _localizer["Ascending"];
        ViewData["NothingWasFound"] = _localizer["NothingWasFound"];
        ViewData["ReadMore"] = _localizer["ReadMore"];
        ViewData["FilterByNickname"] = _localizer["FilterByNickname"];
        ViewData["CreateDiscussion"] = _localizer["CreateDiscussion"];
        ViewData["ProfileOf"] = _localizer["ProfileOf"];
        ViewData["Information"] = _localizer["Information"];
        ViewData["Email"] = _localizer["Email"];
        ViewData["InfoAboutUser"] = _localizer["InfoAboutUser"];
        ViewData["EditProfile"] = _localizer["EditProfile"];
        ViewData["NicknameToChange"] = _localizer["NicknameToChange"];
        ViewData["Password"] = _localizer["Password"];
        ViewData["PFPPath"] = _localizer["PFPPath"];
        ViewData["InfoAboutUser"] = _localizer["InfoAboutUser"];
        ViewData["SaveChanges"] = _localizer["SaveChanges"];
        ViewData["RegisterApply"] = _localizer["RegisterApply"];
        ViewData["Inbox"] = _localizer["Inbox"];
        ViewData["SendPrivateMessage"] = _localizer["SendPrivateMessage"];
        ViewData["ReceiverNickname"] = _localizer["ReceiverNickname"];
        ViewData["Content"] = _localizer["Content"];
        ViewData["SendMessage"] = _localizer["SendMessage"];
        ViewData["SelectGame"] = _localizer["SelectGame"];
        ViewData["TitleOfDiscussion"] = _localizer["TitleOfDiscussion"];
        ViewData["SubmitCreation"] = _localizer["SubmitCreation"];
        ViewData["At"] = _localizer["At"];
        ViewData["LeaveAComment"] = _localizer["LeaveAComment"];
        ViewData["WriteYourCommentHere"] = _localizer["WriteYourCommentHere"];
        ViewData["PostComment"] = _localizer["PostComment"];
        ViewData["LogInTo"] = _localizer["LogInTo"];
        ViewData["ToLeaveAComment"] = _localizer["ToLeaveAComment"];
        ViewData["Comments"] = _localizer["Comments"];
        ViewData["NoCommentsYet"] = _localizer["NoCommentsYet"];
        ViewData["PostedBy"] = _localizer["PostedBy"];
            
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.Nickname = user.Nickname;
                ViewData["SendDM"] = _localizer["SendDM"];
                ViewData["ViewDMs"] = _localizer["ViewDMs"];
                ViewData["Logout"] = _localizer["Logout"];
            }
        }
    }
}