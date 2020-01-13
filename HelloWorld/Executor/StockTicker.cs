using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HelloWorld.Executor
{
    public class StockTicker : IDisposable
    {
        private readonly static Lazy<StockTicker> _instance = new Lazy<StockTicker>(() => new StockTicker());

        private readonly object _updateStockPricesLock = new object();
        private readonly Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(100);
        private readonly Random _updateOrNotUpdate = new Random();
        private readonly double _rangePercent = .002;
        
        public static StockTicker Instance => _instance.Value;

        public List<string> CounterParties { get; }

        public List<string> Symbols { get; }

        private List<MDEntry> _entries;
        public List<MDEntry> Entries => _entries;

        public event Action WasUpdated;

        public StockTicker()
        {
            _entries = new List<MDEntry>
            {
                new MDEntry{ Price=10, Source= "Source1", Type = EntryType.Offer,  Volume ="111" },
                new MDEntry{ Price=110, Source= "Source1", Type = EntryType.Offer,  Volume ="2" },
                new MDEntry{ Price=13, Source= "Source1", Type = EntryType.Offer,  Volume ="22" },
                new MDEntry{ Price=104, Source= "Source2", Type = EntryType.Offer,  Volume ="33" },
                new MDEntry{ Price=105, Source= "Source2", Type = EntryType.Bid,  Volume ="44" },
                new MDEntry{ Price=1230, Source= "Source3", Type = EntryType.Bid,  Volume ="222" },
                new MDEntry{ Price=510, Source= "Source4", Type = EntryType.Bid,  Volume ="111" },
                new MDEntry{ Price=210, Source= "Source4", Type = EntryType.Bid,  Volume ="111" },
            };

            CounterParties = new List<string>
            {
                "Cp1",
                "Cp2"
            };

            Symbols = new List<string>
            {
                "Symbol1",
                "Symbol2"
            };

            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }

        private void UpdateStockPrices(object state)
        {
            lock (_updateStockPricesLock)
            {
                var wasUpdated = false;

                foreach (var entry in Entries)
                {
                    var r = _updateOrNotUpdate.NextDouble();
                    if (r > .1)
                        continue;

                    wasUpdated = true;
                    var random = new Random((int)Math.Floor(entry.Price));
                    var percentChange = random.NextDouble() * _rangePercent;
                    var pos = random.NextDouble() > .51;
                    var change = Math.Round(entry.Price * (decimal)percentChange, 2);
                    change = pos ? change : -change;

                    entry.Price += change;
                }

                if (wasUpdated && WasUpdated != null)
                    WasUpdated.Invoke();
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
