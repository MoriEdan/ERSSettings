using ERSSettings.Dto;
using ERSSettings.Models;
using System;
using System.Collections.Generic;

namespace ERSSettings.Interfaces
{
    internal interface IParentElements
    {
        List<TextedElement> ChildElements { get; set; }

        List<TextedElementDto> ChildsDTO { get; set; }

        void OnChildErrorOccured(TextedElement child, Exception e);
    }
}