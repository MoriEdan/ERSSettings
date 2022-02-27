using ERSSettings.Commons;
using ERSSettings.Dto;
using System;

namespace ERSSettings.Models
{
    internal class CheckBox : TextedElement
    {
        public CheckBox((TextedElementDto Dto, Action<TextedElement, Exception> ErrorHandler, EventHandler<TextedElement> StatusHandler, Func<bool> Customisation, UILanguage Language) parameters) : base(parameters)
        {
        }
    }
}