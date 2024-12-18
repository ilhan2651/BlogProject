using Microsoft.AspNetCore.Mvc;

namespace CoreDemo1.ViewComponents
{
	public class CommentList : ViewComponent
	{
		public async  Task<IViewComponentResult> InvokeAsync()
		{
			
		  return   View(); 
		}
	}
 }

