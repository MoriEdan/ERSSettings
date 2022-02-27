using ERSSettings.Commons;
using ERSSettings.Dto;
using System;

namespace ERSSettings.Models
{
    internal class Switch : TextedElement
    {
        public Switch((TextedElementDto Dto, Action<TextedElement, Exception> ErrorHandler, EventHandler<TextedElement> StatusHandler, Func<bool> Customisation, UILanguage Language) parameters) : base(parameters)
        {
        }
    }
}