using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace VoiceRecognization
{
    class Master
    {
        Nova Nova;
        int CommandCode;
        int retry;
        SpeechRecognitionEngine SREngineChoice;
        SpeechRecognitionEngine SREngineConfirm;
        SpeechRecognitionEngine SREngineStart;
        SpeechSynthesizer Synthesizer;
        public Master(Nova obj)
        {
            this.Nova = obj;
            CommandCode = 0;
            retry = 0;
            SREngineChoice = new SpeechRecognitionEngine();
            SREngineConfirm = new SpeechRecognitionEngine();
            SREngineStart = new SpeechRecognitionEngine();
            Synthesizer = new SpeechSynthesizer();
            Synthesizer.SetOutputToDefaultAudioDevice();
            Begin();
        }


        private void Begin()
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
                    this.Nova.TimeSet.Start();
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
                    this.Nova.rtbLog.Text += "\nSamy";
                    break;
                default:
                    Synthesizer.SpeakAsync("Je n'ai pas compris votre demande");
                    break;
            }
        }

        internal void btnEnable_Click(object sender, EventArgs e)
        {
            SREngineStart.RecognizeAsync(RecognizeMode.Multiple);
            this.Nova.btDisable.Enabled = true;
        }

        internal void btnDisable_Click(object sender, EventArgs e)
        {
            SREngineChoice.RecognizeAsyncCancel();
            SREngineStart.RecognizeAsyncCancel();
            SREngineConfirm.RecognizeAsyncCancel();
            this.Nova.btDisable.Enabled = false;
        }
        private void TimerOut(object sender, EventArgs e)
        {
            if (retry == 2)
            {
                this.Nova.TimeSet.Stop();
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
