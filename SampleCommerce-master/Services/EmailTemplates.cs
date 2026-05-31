namespace SampleCommerce.Services
{
    public static class EmailTemplates
    {
        private static string Wrap(string title, string body) => $@"
<!DOCTYPE html>
<html lang='it'>
<head><meta charset='utf-8'><meta name='viewport' content='width=device-width,initial-scale=1'>
<style>
  body {{ font-family: 'Georgia', serif; background: #f8f4ec; margin: 0; padding: 0; }}
  .wrap {{ max-width: 560px; margin: 0 auto; padding: 48px 24px; }}
  .brand {{ font-size: 11px; letter-spacing: 0.4em; text-transform: uppercase; color: #c9a07a; margin-bottom: 40px; }}
  h1 {{ font-size: 28px; font-weight: 400; color: #1a0f08; line-height: 1.2; margin: 0 0 16px; }}
  p {{ font-size: 16px; color: #3d2414; line-height: 1.8; margin: 0 0 24px; }}
  .btn {{ display: inline-block; background: #1a0f08; color: #f8f4ec; font-family: sans-serif;
          font-size: 11px; letter-spacing: 0.3em; text-transform: uppercase; text-decoration: none;
          padding: 14px 32px; border-radius: 100px; margin: 8px 0 32px; }}
  .note {{ font-size: 13px; color: #6b3f1e; opacity: 0.6; }}
  .divider {{ border: none; border-top: 1px solid rgba(201,160,122,0.3); margin: 32px 0; }}
</style>
</head>
<body>
<div class='wrap'>
  <div class='brand'>Archetipo</div>
  <h1>{title}</h1>
  {body}
  <hr class='divider'>
  <p class='note'>Se non hai richiesto questa email, puoi ignorarla in sicurezza.</p>
</div>
</body>
</html>";

        public static string ConfirmEmail(string name, string link) => Wrap(
            "Conferma la tua email",
            $@"<p>Ciao {System.Web.HttpUtility.HtmlEncode(name)},</p>
               <p>Grazie per esserti registrato su Archetipo. Clicca il pulsante qui sotto per confermare il tuo indirizzo email e attivare il tuo account.</p>
               <a href='{link}' class='btn'>Conferma email →</a>
               <p class='note'>Il link è valido per 24 ore.</p>");

        public static string ResetPassword(string name, string link) => Wrap(
            "Reimposta la tua password",
            $@"<p>Ciao {System.Web.HttpUtility.HtmlEncode(name)},</p>
               <p>Abbiamo ricevuto una richiesta di reimpostazione della password per il tuo account Archetipo. Clicca il pulsante qui sotto per crearne una nuova.</p>
               <a href='{link}' class='btn'>Reimposta password →</a>
               <p class='note'>Il link scade tra 2 ore.</p>");
    }
}
