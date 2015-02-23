using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace GissataletMVC.Models
{
    public enum Outcome
    {
        Low, High, Right, NoMoreGuesses, OldGuess, Undefined
    }
}
