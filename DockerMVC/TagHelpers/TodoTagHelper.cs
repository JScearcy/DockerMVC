using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DockerMVC.TagHelpers
{
    public class TodoTagHelper : TagHelper
    {
        public int TodoId { get; set; }
        public string TodoName { get; set; }
        public bool TodoCompleted { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string checkedAttr = TodoCompleted ? "checked" : string.Empty;
            output.TagName = "div";
            output.Attributes.SetAttribute("data-id", TodoId);
            output.Attributes.SetAttribute("data-name", TodoName);
            output.Attributes.SetAttribute("data-completed", TodoCompleted);
            output.Attributes.SetAttribute("class", "row todo-element");
            output.Content.SetHtmlContent(
                $@"
<input type='checkbox' {checkedAttr} class='col-xs-1 col-sm-1 col-md-1 col-lg-1 col-xl-1 complete-chk' />
<p class='col-xs-8 col-sm-8 col-md-8 col-lg-8 col-xl-8 todo-name'>{TodoName}</p>
<button data-id='{TodoId}' class='btn btn-warning delete-btn'>Delete</button>
            ");
        }
    }
}
