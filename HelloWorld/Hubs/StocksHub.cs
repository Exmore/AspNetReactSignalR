using DotNetify;
using HelloWorld.Executor;
using HelloWorld.Models;
using System.Collections.Generic;

namespace HelloWorld.Hubs
{
    public class StocksHub : BaseVM
    {
        private readonly StockTicker _ticker;
        public List<string> CounterParties => _ticker.CounterParties;
        public List<string> Symbols => _ticker.Symbols;
        public List<MDEntry> Entries => _ticker.Entries;

        public StocksHub():this(StockTicker.Instance)
        {           
        }

        public StocksHub(StockTicker ticker)
        {
            _ticker = ticker;

            _ticker.WasUpdated += () =>
            {
                Changed(nameof(Entries));
                PushUpdates();
            };
        }

        public override void Dispose() => _ticker.Dispose();
    }    
}
