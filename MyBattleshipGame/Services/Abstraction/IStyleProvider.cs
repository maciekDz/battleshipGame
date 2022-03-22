using System;
using MyBattleshipGame.Models;

namespace MyBattleshipGame.Services.Abstraction
{
    public interface IStyleProvider
    {
        void ApplyColumnHeadersStyle(Action printColumnHeaders);

        void ApplyRowHeaderStyle(Action printRowHeader);

        void ApplyNotificationsStyle(Action printNotifications);

        void ApplySquareStyle(ISquare square);

        string WrapElement(string element);
    }
}
