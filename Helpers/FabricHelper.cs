using ERSSettings.Commons;
using ERSSettings.Dto;
using ERSSettings.Interfaces;
using ERSSettings.Models;
using System;

namespace ERSSettings.Helpers
{
    internal class FabricHelper
    {
        private const string MODELS = "ERSSettings.Models";

        private static TextedElement GetTextedElement(TextedElementDto Dto, Action<TextedElement, Exception> ErrorHandler,
                                                                        EventHandler<TextedElement> StatusHandler, UILanguage Language)
        {
            var parameters = (Dto, ErrorHandler, StatusHandler, Customisation: CustomisationsHelper.GetCustomisationStatus(Dto.Id), Language);
            var type = Type.GetType($"{MODELS}.{parameters.Dto.Type}");
            var element = Activator.CreateInstance(type, parameters) as TextedElement;
            return element;
        }

        internal static TextedElement CreateTextedElement(TextedElementDto dto, Action<TextedElement, Exception> errorHandler,
                                                                            EventHandler<TextedElement> statusHandler, UILanguage language)
        {
            var element = GetTextedElement(Dto: dto, ErrorHandler: errorHandler, StatusHandler: statusHandler, Language: language);

            if (element is IParentElements)
            {
                var parent = element as IParentElements;
                parent.ChildsDTO.ForEach(childDto =>
                {
                    var child = GetTextedElement(Dto: childDto, ErrorHandler: parent.OnChildErrorOccured, StatusHandler: statusHandler, Language: language);

                    if (child is RadioButton)
                    {
                        (child as RadioButton).ParentId = element.Id;
                    }

                    parent.ChildElements.Add(child);
                });
            }

            return element;
        }

        internal static UwpElement CreateUwpElement(UwpElementDto dto)
        {
            var type = Type.GetType($"{MODELS}.UwpElement");
            return Activator.CreateInstance(type, dto) as UwpElement;
        }
    }
}