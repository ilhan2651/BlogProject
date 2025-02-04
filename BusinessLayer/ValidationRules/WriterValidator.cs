using EntityLayer.Concrete;
using FluentValidation;

public class WriterValidator : AbstractValidator<Writer>
{
    public WriterValidator()
    {
        RuleFor(w => w.WriterName)
            .NotEmpty().WithMessage("Yazar adı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Yazar adı en az 3 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Yazar adı en fazla 50 karakter olabilir.");

        RuleFor(w => w.WriterAbout)
            .NotEmpty().WithMessage("Yazar hakkında bilgisi boş bırakılamaz.")
            .MinimumLength(10).WithMessage("Yazar hakkında bilgisi en az 10 karakter olmalıdır.");

        RuleFor(w => w.WriterMail)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        // WriterImage boş olabilir ama eğer girildiyse geçerli bir URL olmalı
        RuleFor(w => w.WriterImage)
            .Must(BeAValidUrl).When(w => !string.IsNullOrEmpty(w.WriterImage))
            .WithMessage("Geçerli bir resim URL'si giriniz.");

        // Eğer şifre girilmişse, min 6 karakter olmalı
        RuleFor(w => w.WriterPassword)
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
            .When(w => !string.IsNullOrEmpty(w.WriterPassword));

        // Eğer şifre girilmişse, ConfirmPassword da zorunlu olacak ve eşleşmeli
        RuleFor(w => w.ConfirmPassword)
            .Equal(w => w.WriterPassword)
            .When(w => !string.IsNullOrEmpty(w.WriterPassword))
            .WithMessage("Şifreler eşleşmiyor!");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
