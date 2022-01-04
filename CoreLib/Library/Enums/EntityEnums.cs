using System.ComponentModel;


namespace ErkerCore.Library.Enums
{

    /// <summary>
    /// Kodsal tarafta varlığından haberdar olmak istediğim herşeyi FEATURE KOYUYORUZ , PARENT / PROP YAPISI İLK OLARAK FEATURE TABLOSUNA KOYUYORUZ.
    /// Feature tablosundaki Tablo adlarının Id karşılıkları
    /// </summary>

    public enum Features
    {

        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Banka bilgilerini {logo,mersisno} olarak tutabilen sınıftır.")]
        Bank = 4,
        [DescriptionML("Vergi dairesi bilgilerini {adres sınıfının ilçesine bağ kururak } olarak tutabilen sınıftır.")]
        TaxOffice = 7,
        [DescriptionML("Muhatap Tipi")]
        ContactType = 8,
        [DescriptionML("Adres")]
        Adres = 9,
        [DescriptionML("Günler")]
        Days = 10,
        [DescriptionML("Feature'ları validate etmek için kullanıyoruz")]
        ValidationRules = 11,
        [DescriptionML("Tablo")]
        Table = 21,
        [DescriptionML("Dil")]
        Language = 49,

        [DescriptionML("Hata Enumları")]
        ValidationErrorEnum = 25,
        [DescriptionML("-")]
        FormController = 43,
        [DescriptionML("Abone lisans bilgisi için lisansın hangi tipte olduğunu belirtmek için kullanılır.")]
        LicenseType = 58,
        [DescriptionML("Sektör")]
        Sector = 62,
        [DescriptionML("Alt Muhatap Bağlantı Türleri")]
        ContactRelation = 73,

        [DescriptionML("Ürün Tipleri")]
        ProductType = 83,

        [DescriptionML("Ürün Kategorileri")]
        ProductCategory = 84,
        [DescriptionML("Ürün Markaları")]
        ProductBrand = 85,
        [DescriptionML("Ürün Modelleri")]
        ProductModel = 86,
        [DescriptionML("Ürün Versiyonları")]
        ProductVersion = 140,
        [DescriptionML("Çalışan Sayısı")]
        ContactEmployeeCount = 87,
        [DescriptionML("Dil Desteği Anahtarları")]
        TranslateKey = 88,
        [DescriptionML("Dosya Türleri")]
        FileType = 89,
        [DescriptionML("Departman")]
        Department = 96,
        [DescriptionML("Para Birimleri")]
        CurrencyType = 101,
        [DescriptionML("Ölçü Birimleri")]
        MeasureOfUnit = 106,

        [DescriptionML("Benzer Ürünler")]
        ProductSimilar = 111,
        [DescriptionML("İlişkili Ürünler")]       // 1 ürün ile 2 nolu ProductSimilar
        ProductRelated = 112,

        [DescriptionML("Ürün Çoklu Kategori")]
        ProductMultiCategory = 113,

        [DescriptionML("Muhatap Bağlantı Verileri")]
        ContactRelationData = 2712,
        [DescriptionML("Ürün Reçete Verileri")]

        ProductPart = 114,
        [DescriptionML("Flex ")]
        FlexAction = 10001,

        [DescriptionML("Contact Tipi Tema", "")]
        ContactTypeSettings = 2713,

        [DescriptionML("Zimmmet Edilen")]
        ContactEmbezzled = 2718,
        [DescriptionML("Acil durum kişisi")]
        ContactEmergency = 2719,
        [DescriptionML("İstihdam durumu")]
        EmploymentEntry = 2720,
        [DescriptionML("İstihdam türü")]
        EmploymentType = 2721,
        [DescriptionML("İstihdam pozisyonu")]
        WorkPosition = 2722,
        [DescriptionML("Ayrılış türü")]
        ResignType = 2723,
        [DescriptionML("Kan grubu")]
        BloodGroup = 2724,
        [DescriptionML("Askerlik durumu")]
        MilitaryService = 2725,
        [DescriptionML("Evlilik durumu")]
        MaritalStatus = 2726,
        [DescriptionML("Engel durumu")]
        DisabilityStatus = 2727,
        [DescriptionML("Eş çalışma durumu")]
        SpouseJobStatus = 2728,
        [DescriptionML("Ürün Tedarikçi Verisi")]
        ProductSupplierData = 2755,
        [DescriptionML("Müşteri Ürün Takibi")]
        ContactProductTrack = 2756,
        [DescriptionML("Servis Olay durumları")]
        ServiceStatus = 2757,
        [DescriptionML("İş Emri Türü")]
        WorkOrderType = 2765,
        [DescriptionML("Arıza Nedeni")]
        FailureCause = 2766,
        [DescriptionML("Ürün Müşteri Takibi")]
        ProductContactTrack = 2767,
        [DescriptionML("Bağlı Ekipmanlar")]
        LinkedEquipment = 2768,


        [DescriptionML("Durum")]
        EventStatus = 2770,
        [DescriptionML("Durum Teması")]
        EventStatusSettings = 2772,
        [DescriptionML("Modüller")]
        Module = 2773,
        [DescriptionML("Kur Geçmişi")]
        CurrencyHistory = 2781,
    }
    public enum FeatureModule
    {

        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Generic Modül", "Generic Module")]
        Generic = 2800,
        [DescriptionML("Servis", "Service")]
        Service = 2774,
        [DescriptionML("Ürün", "Product")]
        Product = 2775,
        [DescriptionML("Muhatap", "Contact")]
        Contact = 2776,
        [DescriptionML("Teklif", "Offer")]
        Offer = 275629,
        [DescriptionML("Müşteri Ürün Takibi", "ContactProductTrack")]
        ContactProductTrack = 2777
    }

    public enum FeatureValidationErrorEnum
    {

        //31 35 36
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Request null olamaz.")]
        RequestCannotBeNull = 31,
        [DescriptionML("Object null olamaz.")]
        ObjectCannotBeNull = 82,
        [DescriptionML("Değer 0 olamaz.")]
        IdFieldCannotBeLtEqZero = 35,
        [DescriptionML("Alan boş bırakılamaz.")]
        FieldCannotBeEmptyOrNull = 36,

        [DescriptionML("Değer 0 dan büyük olmalıdır.")]
        PriceOrAmountValueMustGreaterThanZero = 37,

        [DescriptionML("Kullanıcı Adı veya Şifre Geçersiz")]
        UserNameOrPassswordInCorrect = 40,
        [DescriptionML("Kullanıcı Adı çok kısa")]
        UserNameTooShort = 42,
        [DescriptionML("Şifre çok kolay")]
        PasswordTooEasy = 44,

        [DescriptionML("Yetkiniz yok")]
        NotAuthorize = 130,
        [DescriptionML("Kullanıcı zaten kayıtlı")]
        UserAlreadyRegistered = 129,
        [DescriptionML("Zaten Silindi")]
        AlreadyDeleted = 131,

        [DescriptionML("Maksimum Değer Aşımı ")]
        MaximumValueError = 132,

        [DescriptionML("Minimum Değer Aşımı ")]
        MinimumValueError = 134,
        [DescriptionML("Minimum Uzunluk Aşımı ")]
        MinimumLengthError = 133,
        [DescriptionML("Maksimum Değer Aşımı ")]
        MaximumLengthError = 135,
        [DescriptionML("Geçersiz değer")]
        InvalidValueError = 136,
        [DescriptionML("Aralık Değerler Geçersiz")]
        BetweenValueError = 137,
        [DescriptionML("Kayıt Bulunamadı")]
        NotFoundRecord = 138,
        [DescriptionML("Kayıt Zaten Var")]
        AlreadExistRecord = 126,

        [DescriptionML("Güncelleme hatası")]
        UpdateError = 789654,
    }

    [Module(FeatureModule.Service)]
    public enum FeatureServiceStatus
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Servis alındı")]
        ServiceReceived = 2758,

        [DescriptionML("Arıza onarımda")]
        InRepair = 2759,

        [DescriptionML("Yetkili serviste")]

        AtAuthorizedService = 2760,
        [DescriptionML("Fabrika iadesi bekleniyor")]
        FactoryReturnPending = 2761,
        [DescriptionML("Test aşamasında")]
        InTesting = 2762,
        [DescriptionML("Kurulum yapılacak")]
        InstallationWillBeDone = 2763,
        [DescriptionML("Servis tamamlandı")]
        ServiceCompleted = 2764,
    }

    public enum FeatureFlexAction
    {
        [DescriptionML("Belirsiz", "Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Adres Detayları Görüntüleme", "Address Detail Show")]
        Create = 4515153,



        [Module(FeatureModule.Product)]
        [DescriptionML("Ürün Ekleme", "Product Create")]
        ProductCreate = 4515151,

        [Module(FeatureModule.Product)]

        [DescriptionML("Ürün Ekleme", "Product Create")]

        ProductUpdate = 45151532,
        [DescriptionML("Adres Detayları Görüntüleme", "Address Detail Show")]

        Update = 4515152,

        [DescriptionML("Adres Detayları Görüntüleme", "Address Detail Show")]
        GetAdressDetail = 20000,
        [DescriptionML("İl ilçe Getir", "GetRelatedCityDistrictCountry")]
        GetRelatedCityDistrictCountry = 20001,

    }
    public enum FeatureDepartment
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Muhasebe")]
        Accounting = 97,
        [DescriptionML("Finans")]
        Finance = 98
    }
    public enum FeatureContactEmployeeCount
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("1-5 Çalışan")]
        OneToFive = 90,
        [DescriptionML("5-10 Çalışan")]
        FiveToTen = 91,
        [DescriptionML("10-25 Çalışan")]
        TenToTwentyFive = 92,
        [DescriptionML("25-50 Çalışan")]
        TwentyFiveToFifty = 93,
        [DescriptionML("50-100 Çalışan")]
        FiftyToHundred = 94,
        [DescriptionML("100+ Çalışan")]
        HundredToMax = 95

    }
    public enum FeatureCurrencyType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("TL")]
        TurkishLira = 102,
        [DescriptionML("€")]
        Euro = 103,
        [DescriptionML("$")]
        USDolar = 104,
        [DescriptionML("£")]
        Sterlin = 105,
    }
    public enum FeatureProductType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,

        [DescriptionML("Ürün")]
        Product = 425789,
        [DescriptionML("Aksesuar")]
        Accessories = 115,
        [DescriptionML("Hizmet")]
        Service = 99,

    }
    public enum FeatureTaxOffice
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("İl")]
        City = 68,
        [DescriptionML("İlçe")]
        Town = 69
    }
    public enum FeatureFileType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Döküman")]
        Document = 117,
        [DescriptionML("Resim")]
        Image = 118,
        [DescriptionML("Video")]
        Video = 120,
        [DescriptionML("Diğer")]
        Other = 121
    }

    public enum FeatureSector
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Otomotiv")]
        Automotive = 63,
        [DescriptionML("Gıda")]
        Food = 64,
        [DescriptionML("Kimya")]
        Chemistry = 65,
        [DescriptionML("Finans")]
        Finance = 66,
        [DescriptionML("Tekstil")]
        Textile = 67,
    }
    public enum FeatureAdres
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Ülke")]
        Country = 1,
        [DescriptionML("İl")]
        City = 2,
        [DescriptionML("İlçe")]
        District = 3,
    }
    public enum FeatureMeasureOfUnit
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Adet")]
        Quantity = 107,
        [DescriptionML("Kilogram")]
        Kilogram = 108,
        [DescriptionML("Metre")]
        Meter = 109,
    }
    public enum FeatureTranslateKey
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Ürün Adı")]
        ProductName = 122,
        [DescriptionML("Ürün Açıklaması")]
        ProductDescription = 124,
        [DescriptionML("Ürün Başlığı")]
        ProductSeoTitle = 16842,
        [DescriptionML("Ürün Seo Açıklama", "Product Seo Description")]
        ProductSeoDescription = 12600,
        [DescriptionML("Ürün SEO Anahtar Kelimeler")]
        ProductSeoKeywords = 123,

        /*[DescriptionML("ilerisi bir örnek kayıt : bir tablonun feature olmayan bir alanı için nasıl dil desteği sağlayabileceğimizin örneğidir..")]
           ContactGender = 987    */
    }
    public enum FeatureProductStatus
    {
        [DescriptionML("Belirsiz", "Uncertain")]
        Uncertain = -1,
        [DescriptionML("Parça Bekliyor", "Waiting Part")]
        WaitingPart = 451545


    }

    public enum FeatureFileKey
    {  /*[DescriptionML("ilerisi bir örnek kayıt : bir tablonun feature olmayan bir alanı için nasıl dil desteği sağlayabileceğimizin örneğidir..")]
           ContactGender = 987    */
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("ProductThumbnail")]
        ProductThumbnail = 1641650,
        [DescriptionML("ProductOriginal")]
        ProductOriginal = 1656461,
        [DescriptionML("ProductThumbnail100x100")]
        ProductThumbnail100x100 = 16275762,
        [DescriptionML("ProductThumbnail50x50")]
        ProductThumbnail50x50 = 167727563,
        [DescriptionML("ContactProfile")]
        ContactProfile = 16897784,
        [DescriptionML("ContactProfileThumbnail")]
        ContactProfileThumbnail = 1678625,
        [DescriptionML("ContactPersonProfileThumbnail")]
        ContactPersonProfileThumbnail = 1678650


    }
    public enum FeatureLicenseType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Lisans tipi 'Kiralık' olduğunu belirtir.")]
        Rental = 59,
        [DescriptionML("Lisans tipi 'Satılık' olduğunu belirtir.")]
        ForSale = 60,
        [DescriptionML("Lisans tipi 'Demo' olduğunu belirtir.")]
        Demo = 61,
    }

    public enum FeatureDays
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Pazartesi")]
        Monday = 12,
    }
    public enum IsLogic
    {

        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Evet")]
        Yes = 1,
        [DescriptionML("Hayır")]
        No = 0,
    }
    public enum FeatureValidationRules
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Maksimum")]
        Maximum = 13,
        [DescriptionML("Minimum")]
        Minimum = 14,
        [DescriptionML("Arasında")]
        Between = 15,
        [DescriptionML("Karakter uzunluğu")]
        CharacterLength = 16,
        [DescriptionML("Gerekli mi?")]
        IsRequired = 17,
        [DescriptionML("Özel maske")]
        CustomInputMask = 18,
        [DescriptionML("Öntanımlı maske")]
        PredefinedInputMasks = 19,
        [DescriptionML("Sistemin sunduğu veya kullanıcının belirlediği \"çoktan seçmeli datalarda\" varsayılan hangisi seçili geleceğini tutacak olan özelliktir.")]
        DefaultSelection = 20,
    }

    public enum FeatureProductCategory
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Elektronik")]
        Electronic = 100

    }


    public enum FeatureContactType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Birey")]
        Individual = 26,
        [DescriptionML("Şirket")]
        Company = 27,
        [DescriptionML("Kurum")]
        Organization = 28,
        [DescriptionML("Şube")]
        Branch = 29,
        [DescriptionML("Bayi")]
        Franchise = 30,
        [DescriptionML("Müşteri Adayı")]
        CustomerCandidate = 38,
        [DescriptionML("Tedarikçi Adayı")]
        SupplierCandidate = 39,
        [DescriptionML("Tedarikçi")]
        Supplier = 41,
        [DescriptionML("Müşteri")]
        Customer = 76645,
    }
    public enum FeatureEmploymentType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Tam Zamanlı")]
        FullTime = 2729,
        [DescriptionML("Yarı Zamanlı")]
        PartTime = 2730,
        [DescriptionML("Stajyer")]
        Intern = 2731,
        [DescriptionML("Dış Kaynak")]
        OutSource = 2732
    }
    public enum FeatureWorkPosition //feature olacak bu.
    {

    }
    public enum FeatureResignType
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("İstifa")]
        Resignation = 2733,
        [DescriptionML("İşten çıkarılma")]
        Dissmissed = 2734,
        [DescriptionML("Ücretsiz izin")]
        UnpaidVacation = 2735,
    }
    public enum FeatureBloodGroup
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("A pozitif")]
        PositiveA = 2736,
        [DescriptionML("A Negatif")]
        NegativeA = 2737,
        [DescriptionML("B pozitif")]
        PositiveB = 2738,
        [DescriptionML("B Negatif")]
        NegativeB = 2739,
        [DescriptionML("AB pozitif")]
        PositiveAB = 2740,
        [DescriptionML("AB Negatif")]
        NegativeAB = 2741,
        [DescriptionML("0 pozitif")]
        Positive0 = 2742,
        [DescriptionML("0 Negatif")]
        Negative0 = 2743,
    }
    public enum FeatureMilitaryService
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Yapıldı")]
        Done = 2744,
        [DescriptionML("Muaf")]
        Exempted = 2745,
        [DescriptionML("Tecilli")]
        Delayed = 2746,
    }
    public enum FeatureMaritalStatus
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Evli")]
        Married = 2747,
        [DescriptionML("Bekar")]
        Single = 2748,
        [DescriptionML("Diğer")]
        Other = 2749,
    }
    public enum FeatureDisabilityStatus
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Engelli değil")]
        NonDisabled = 2750,
        [DescriptionML("Kısmi engelli")]
        SemiDisabled = 2751,
        [DescriptionML("Engelli")]
        Disabled = 2752,
    }
    public enum FeatureSpouseJobStatus
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Çalışıyor")]
        Working = 2753,
        [DescriptionML("Çalışmıyor")]
        Notworking = 2754,
    }

    public enum FeatureTable
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Contact")]
        Contact = 22,
        [DescriptionML("Subscriber")]
        Subscriber = 23,
        [DescriptionML("Product")]
        Product = 613516,
        [DescriptionML("Component")]
        Component = 116551,
        [DescriptionML("Feature")]
        Feature = 11655452,
        [DescriptionML("FeatureValue")]
        FeatureValue = 116253,
        [DescriptionML("ContactPerson")]
        ContactPerson = 2769
    }

    public enum FeatureLanguage
    {
        [DescriptionML("Belirsiz")]
        Uncertain = -1,
        [DescriptionML("Türkçe")]
        Tr = 50,
        [DescriptionML("En-Us")]
        EnUs = 51,
        [DescriptionML("En-UK")]
        EnUK = 52,
        [DescriptionML("Es")]
        Es = 53,
        [DescriptionML("It")]
        It = 54,
        [DescriptionML("Fr")]
        Fr = 55,
        [DescriptionML("De")]
        De = 56,
        [DescriptionML("Bulgarca")]
        Bg = 57,

    }

    public enum OperationType
    {
        [DescriptionML("Create")]
        C,
        [DescriptionML("Update")]
        U,
        [DescriptionML("Delete")]
        D,
        [DescriptionML("SoftDelete")]
        S

    }
}
