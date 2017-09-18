using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace VoiceRecognization
{
    class Choice
    {
        int CommandCode = 0;
        int retry = 0;
        SpeechRecognitionEngine SREngineChoice = new SpeechRecognitionEngine();
        SpeechRecognitionEngine SREngineConfirm = new SpeechRecognitionEngine();
        SpeechRecognitionEngine SREngineStart = new SpeechRecognitionEngine();
        SpeechSynthesizer Synthesizer = new SpeechSynthesizer();

        private void init()
        {
            Choices choice = new Choices();
            Choices confirm = new Choices();
            Choices start = new Choices();
            start.Add(new String[] { "Nova", "Hey Nova" });
            confirm.Add(new String[] { "Oui", "Non" });
            choice.Add(new String[] { "ouvre opéra", " quelle est mon nom", "Yes" });
            GrammarBuilder ConfirmgBuilder = new GrammarBuilder(confirm);
            GrammarBuilder ChoicegBuilder = new GrammarBuilder(choice);
            GrammarBuilder StartgBuilder = new GrammarBuilder(start);
            Grammar StartGrammar = new Grammar(StartgBuilder);
            Grammar ChoiceGrammar = new Grammar(ChoicegBuilder);
            Grammar ConfirmGrammar = new Grammar(ConfirmgBuilder);

            SREngineStart.LoadGrammarAsync(StartGrammar);
            SREngineStart.SetInputToDefaultAudioDevice();
            SREngineStart.SpeechRecognized += SREngineStart_SpeechRecognized;

            SREngineChoice.LoadGrammarAsync(ChoiceGrammar);
            SREngineChoice.SetInputToDefaultAudioDevice();
            SREngineChoice.SpeechRecognized += SREngineChoice_SpeechRecognized;

            SREngineConfirm.LoadGrammar(ConfirmGrammar);
            SREngineConfirm.SetInputToDefaultAudioDevice();
            SREngineConfirm.SpeechRecognized += SREngineConfirm_SpeechRecognized;
        }

        void SREngineStart_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Nova":
                case "Hey Nova":
                    retry = 0;
                    Synthesizer.SpeakAsync("En quoi puis-je vous aider?");
                    SREngineStart.RecognizeAsyncCancel();
                    SREngineChoice.RecognizeAsync(RecognizeMode.Multiple);
                    TimeSet.Start();
                    break;
            }
        }

        void SREngineConfirm_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Oui":
                    SREngineConfirm.RecognizeAsyncCancel();
                    if (CommandCode == 1)
                        System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Opera\\launcher.exe");
                    else
                        MessageBox.Show("Erreur");
                    SREngineStart.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "Non":
                    SREngineConfirm.RecognizeAsyncCancel();
                    Synthesizer.SpeakAsync("Opera ne se lancera pas");
                    SREngineStart.RecognizeAsync(RecognizeMode.Multiple);
                    break;
            }

        }

        void SREngineChoice_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "ouvre opéra":
                    SREngineChoice.RecognizeAsyncCancel();
                    CommandCode = 1;
                    Synthesizer.SpeakAsync("êtes-vous sur?");
                    SREngineConfirm.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "quelle est mon nom":
                    rtbLog.Text += "\nSamy";
                    break;
                default:
                    Synthesizer.SpeakAsync("Je n'ai pas compris votre demande");
                    break;
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            SREngineStart.RecognizeAsync(RecognizeMode.Multiple);
            btDisable.Enabled = true;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            SREngineChoice.RecognizeAsyncCancel();
            SREngineStart.RecognizeAsyncCancel();
            SREngineConfirm.RecognizeAsyncCancel();
            btDisable.Enabled = false;
        }
        private void TimerOut(object sender, EventArgs e)
        {
            if (retry == 2)
            {
                TimeSet.Stop();
                SREngineChoice.RecognizeAsyncCancel();
                SREngineStart.RecognizeAsyncCancel();
                Synthesizer.SpeakAsync("Je n'ai pas compris votre demande, je me rendors.");
                SREngineStart.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
                Synthesizer.SpeakAsync("Je n'ai pas compris votre demande, réessayer.");
            retry++;

        }
    }
}
