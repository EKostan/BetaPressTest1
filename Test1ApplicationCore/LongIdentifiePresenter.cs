using System;
using LongIdentifierLibrary;
using NLog;

namespace Test1ApplicationCore
{
    public class LongIdentifiePresenter
    {
        private readonly ILongIdentifierView _view;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public LongIdentifiePresenter(ILongIdentifierView view)
        {
            _view = view;
            SignViewEvents();
        }

        private void SignViewEvents()
        {
            _view.IncrementIdentifier += ViewIncrementIdentifier;
            _view.InitAlphaNumericIdentifier += ViewInitAlphaNumericIdentifier;
        }

        private void ViewInitAlphaNumericIdentifier(object sender, EventArgs e)
        {
            try
            {
                IdController.InitAlphaNumericIdentifier(_view.AlphaNumericIdentifier);
                ShowMessage($"ID изменен на {IdController.Identifier}");
            }
            catch (Exception exception)
            {
                ShowError($"Ошибка: {exception.Message}");
            }
        }

        private void ShowMessage(string message)
        {
            _view.ShowMessage(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " - " + message);
            _logger.Info(message);
        }

        private void ShowError(string error)
        {
            _view.ShowMessage(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " - " + error);
            _logger.Error(error);
        }

        private void ViewIncrementIdentifier(object sender, EventArgs e)
        {
            try
            {
                IdController.Increment(_view.IncrementValue);
                ShowMessage($"ID инкрементирован на {_view.IncrementValue} и ID = {IdController.Identifier}");
            }
            catch (Exception exception)
            {
                ShowError($"Ошибка: {exception.Message}");
            }
        }
    }
}