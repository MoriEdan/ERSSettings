using ERSSettings.Commons;
using ERSSettings.Dto;
using System;

namespace ERSSettings.Models
{
    internal class RadioButton : TextedElement
    {
        public RadioButton((TextedElementDto Dto, Action<TextedElement, Exception> ErrorHandler, EventHandler<TextedElement> StatusHandler, Func<bool> Customisation, UILanguage Language) parameters) : base(parameters)
        {
        }

        internal uint ParentId { get; set; }
    }
}