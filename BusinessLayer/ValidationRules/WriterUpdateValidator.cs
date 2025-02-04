using EntityLayer.Concrete;
using FluentValidation;

public class WriterUpdateValidator : AbstractValidator<Writer>
{
    public WriterUpdateValidator()
    {
        RuleFor(w => w.WriterName)
            .NotEmpty().WithMessage("Yazar adı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Yazar adı en az 3 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Yazar adı en fazla 50 karakter olabilir.");

        RuleFor(w => w.WriterMail)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(w => w.WriterAbout)
            .NotEmpty().WithMessage("Yazar hakkında bilgisi boş bırakılamaz.")
            .MinimumLength(10).WithMessage("Yazar hakkında bilgisi en az 10 karakter olmalıdır.");


        RuleFor(w => w.WriterPassword)
            .NotEmpty().When(w => !string.IsNullOrEmpty(w.ConfirmPassword))
            .WithMessage("Yeni şifre girildiğinde doğrulama şifresi de girilmelidir.");

        RuleFor(w => w.ConfirmPassword)
            .NotEmpty().When(w => !string.IsNullOrEmpty(w.WriterPassword))
            .WithMessage("Yeni şifre doğrulaması girilmelidir.");

        RuleFor(w => w.WriterPassword)
            .Equal(w => w.ConfirmPassword)
            .When(w => !string.IsNullOrEmpty(w.WriterPassword) || !string.IsNullOrEmpty(w.ConfirmPassword))
            .WithMessage("Şifreler eşleşmiyor!");
    }
}
