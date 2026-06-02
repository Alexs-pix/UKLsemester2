List<Stand> data_stand = new List<Stand>()
{
    new StandOutdoor ("\nOutdoor-1", 400000),
    new StandOutdoor ("Outdoor-2", 500000),
    new StandIndoor ("Indoor-1", 700000),
    new StandIndoor ("Indoor-2", 800000),
    new StandPremium ("Premium-1", 1800000),
    new StandPremium ("Premium-2", 2000000),
};

while (true)
{
    Console.Clear();

    Console.WriteLine("=== Moklet Expo Management Center ===");
    Console.WriteLine("Daftar Stand Tersedia");

    foreach (var dk in data_stand)
    {
        dk.tampilkanInfo();
    }

    Console.WriteLine("\n1. Sewa Stand \n2. Akhiri Sewa Stand \n3. Keluar");
    Console.WriteLine("\nPilih Menu: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1") // Proses penyewaan stand
    {
        Console.Write("\nInput nama stand: ");
        string namaStand = Console.ReadLine();

        var cari_stand = data_stand.FirstOrDefault(cs => string.Equals(namaStand, cs.namaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_stand == null)
        {
            Console.WriteLine("\nStand tidak ditemukan");
        }
        else if (cari_stand.isAvailable)
        {
            Console.Write("\nInput jumlah hari sewa: ");
            int hari = int.Parse(Console.ReadLine());

            double total_sewa = cari_stand.hitungTotal(hari);

            cari_stand.ubahStatus();

            Console.WriteLine($"Total pembayaran sewa: Rp {total_sewa}");
        }
        else
        {
            Console.WriteLine("\nStand sedang tidak tersedia");
        }
    }
    else if (pilihan == "2") // Proses pengembalian stand
    {
        Console.Write("\nInput nama stand: ");
        string namaStand = Console.ReadLine();

        var cari_stand = data_stand.FirstOrDefault(cs => string.Equals(namaStand, cs.namaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_stand == null)
        {
            Console.WriteLine("\nStand tidak ditemukan");
        }
        else if (!cari_stand.isAvailable)
        {
            cari_stand.ubahStatus();
            Console.WriteLine("\nSewa stand berhasil diakhiri");
        }
        else
        {
            Console.WriteLine("\nProses pengembalian tidak bisa dilakukan");
        }
    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\nTekan E N T E R untuk menutup aplikasi...");
        Console.ReadLine();
        break;
    }
    else
    {
        Console.WriteLine("\nPilihan Invalid!");
    }

    Console.WriteLine("\nTekan E N T E R untuk mengulangi proses...");
    Console.ReadLine();
}

class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _isAvailable;

    public Stand (string nama_stand, double harga_Sewa)
    {
        _namaStand = nama_stand;
        _hargaSewaPerHari = harga_Sewa;
        _isAvailable = true;
    }

    public string namaStand
    {
        get { return _namaStand; }
        set
        {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Nama tidak boleh kosong");
                }

                else
                {
                    _namaStand = value;
                }
        }
    }

    public double hargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value > 0)
            {
                hargaSewaPerHari = value;
            }

            else
            {
                Console.WriteLine("Harga Sewa Harus Lebih Besar Dari 0");
            }
        }
    }

    public bool isAvailable
    {
        get { return _isAvailable; }
    }

    public void tampilkanInfo()
    {
        Console.WriteLine($"{_namaStand} | Rp {_hargaSewaPerHari} / hari | {(_isAvailable ? "Tersedia" : "Tidak tersedia")} ");
    }

    public void ubahStatus()
    {
        _isAvailable = !_isAvailable;
    }

    public virtual double hitungTotal (int jumlahHari)
    {
        return _hargaSewaPerHari * jumlahHari;
    }
}

class StandOutdoor : Stand
{
    protected double _biayaTenda = 75000;

    public StandOutdoor (string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
        _biayaTenda = 75000;
    }
    public double biayaTenda
    {
        get { return _biayaTenda; }
    }

    public override double hitungTotal(int jumlahHari)
    {
        return base.hitungTotal(jumlahHari) + (_biayaTenda * jumlahHari);
    }
}

class StandIndoor : Stand
{
    protected double _biayaListrik = 100000;

    public StandIndoor (string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
        _biayaListrik = 100000;
    }
    public double biayaListrik
    {
        get { return _biayaListrik; }
    }

    public override double hitungTotal(int jumlahHari)
    {
        return base.hitungTotal(jumlahHari) + (_biayaListrik * jumlahHari);
    }
}

class StandPremium : Stand
{
    protected double _biayaKeamanan = 300000;

    public StandPremium (string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
        _biayaKeamanan = 100000;
    }
    public double biayaKeamanan
    {
        get { return _biayaKeamanan; }
    }

    public override double hitungTotal(int jumlahHari)
    {
        return base.hitungTotal(jumlahHari) + _biayaKeamanan;
    }
}