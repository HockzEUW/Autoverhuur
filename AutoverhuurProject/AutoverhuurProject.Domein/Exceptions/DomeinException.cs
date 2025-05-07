using System.Runtime.Serialization;

namespace AutoverhuurProject.Domein.Exceptions;

public class DomeinException : Exception {

    public DomeinException() {

    }

    public DomeinException(string? message) : base(message) {

    }

    public DomeinException(string? message, int lijnNummer) : base (message) {

    }

    public DomeinException(string? message, Exception? innerException) : base(message, innerException) {

    }
}