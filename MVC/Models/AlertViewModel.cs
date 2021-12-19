using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class AlertViewModel
    {
        public AlertType Type { get; set; }
        public string Message { get; set; }

        public string Class {
            get
            {
                return (Type == AlertType.Success) ? "success" : "danger";
            }
        }

        public string Icon
        {
            get
            {
                return (Type == AlertType.Success) ? "check-circle-fill" : "exclamation-triangle-fill";
            }
        }
    }
}
