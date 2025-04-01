using System.Collections.Generic;
using UnityEngine;

public static class EylemUretici
{
    private static Dictionary<string, List<string>> iyiEylemler = new Dictionary<string, List<string>>()
    {
        {"kolay", new List<string>{
            "Kan bağışında bulundu",
            "Çevre temizliğine katıldı",
            "Kütüphaneye kitap bağışladı",
            "Yaşlılara yemek dağıttı",
            "Hayvan barınağında gönüllü çalıştı",
            "Hasta çocuğu hastaneye götürdü",
            "Gönüllü olarak okul boyadı",
            "Ağaç dikim kampanyasına katıldı",
            "Mahallede sağlık taraması yaptı",
            "Engelli komşusuna yardım etti"
        }},
        {"orta", new List<string>{
            "Sokak çocuklarına sıcak yemek dağıttı",
            "Okul kantinine ücretsiz sağlıklı atıştırmalık koydu",
            "Hayvanlara su kapları yerleştirdi",
            "Köy okuluna kırtasiye malzemesi gönderdi",
            "Hasta annesine işten izin alıp baktı",
            "İhtiyaç sahibi öğrencilerin aidatlarını ödedi",
            "Depremzedelere kıyafet yardımında bulundu",
            "Çocuklara kodlama eğitimi verdi",
            "Otobüste yaşlıya yer verdi",
            "Kanser hastası komşusuna refakat etti"
        }},
        {"zor", new List<string>{
            "Uyuşturucu bağımlısına iş buldu ve yardım etti",
            "Rüşvetle iş kazanmış ama sonra bağış yapmış",
            "Ailesini terk etti ama sonra barıştırdı",
            "Alkol problemiyle mücadele edip bilinçlendirdi",
            "Zorbalık yapan kişiye terapi desteği aldı",
            "Kaçak çalışan işçileri eğitime yönlendirdi",
            "İftira atan kişiye affedici davrandı",
            "İhmal edilen çocukları korumaya aldı",
            "İnsanların güvenini yeniden kazandı",
            "İşlediği suçu itiraf ederek adalete teslim oldu"
        }}
    };

    private static Dictionary<string, List<string>> kotuEylemler = new Dictionary<string, List<string>>()
    {
        {"kolay", new List<string>{
            "Rüşvet aldı",
            "Sahte belge düzenledi",
            "Dolandırıcılık yaptı",
            "Sigara yasağını ihlal etti",
            "Uyuşturucu sattı",
            "Vergi kaçırdı",
            "Hırsızlık yaptı",
            "Kamu malına zarar verdi",
            "Trafikte alkollü yakalandı",
            "Sosyal medyada nefret söylemi yaydı"
        }},
        {"orta", new List<string>{
            "Trafikte diğer sürücülere küfür etti",
            "İş yerinde çalışanlara baskı uyguladı",
            "Kendine çıkar sağlamak için iftira attı",
            "Sınavda kopya verdi",
            "Vergi beyannamesinde yalan söyledi",
            "Birine bilerek zarar verdi",
            "Grup sohbetinde kişiye hakaret etti",
            "Yardıma muhtaç kişiyi aşağıladı",
            "Mahallede kavga çıkardı",
            "Toplum önünde alkol kullandı"
        }},
        {"zor", new List<string>{
            "Bağış topladı ama parayı kendi harcadı",
            "Terk ettiği çocuğunu yıllar sonra reddetti",
            "Zor durumda olanı dolandırdı",
            "İyilik yapıyor gibi davranıp reklam yaptı",
            "Yardım kampanyasında yalan söyledi",
            "Uyuşturucudan tedavi görüp yeniden başladı",
            "Alkol bağımlılığı bahanesiyle şiddet uyguladı",
            "Psikolojik manipülasyonla çıkar sağladı",
            "Aile içi şiddeti gizledi",
            "Toplum desteğini kötüye kullandı"
        }}
    };

    public static List<string> RastgeleEylemlerUret(string zorluk)
    {
        List<string> eylemler = new List<string>();

        var iyi = new List<string>(iyiEylemler[zorluk]);
        var kotu = new List<string>(kotuEylemler[zorluk]);

        for (int i = 0; i < 4; i++) // 4 iyi eylem
        {
            int index = Random.Range(0, iyi.Count);
            eylemler.Add(iyi[index]);
            iyi.RemoveAt(index);
        }

        for (int i = 0; i < 3; i++) // 3 kötü eylem
        {
            int index = Random.Range(0, kotu.Count);
            eylemler.Add(kotu[index]);
            kotu.RemoveAt(index);
        }

        // Karışık hale getir
        for (int i = 0; i < eylemler.Count; i++)
        {
            int randIndex = Random.Range(0, eylemler.Count);
            var temp = eylemler[i];
            eylemler[i] = eylemler[randIndex];
            eylemler[randIndex] = temp;
        }

        return eylemler;
    }
}
