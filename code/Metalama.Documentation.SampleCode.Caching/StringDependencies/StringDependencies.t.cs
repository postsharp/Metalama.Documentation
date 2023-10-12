// Program execution failed
System.Reflection.TargetInvocationException :
Exception has been  thrown  by  the  target  of  an  invocation . --  -> Xunit . Sdk . EqualException  :
Assert.Equal()Failure Expected :
2 Actual:
  1 at Xunit .Assert.Equal[T](T expected, T actual, IEqualityComparer `1 comparer) in
C:
   \
Dev \ xunit \ xunit \ src \ xunit . assert \ Asserts \ EqualityAsserts . cs :
line  40  at  Xunit . Assert . Equal [T]
(T expected, T actual) in
C:
   \
Dev \ xunit \ xunit \ src \ xunit . assert \ Asserts \ EqualityAsserts . cs :
line  24  at  Doc . StringDependencies . ConsoleMain . Execute (  )
at Metalama .Documentation.Helpers.ConsoleApp.ConsoleApp.Run() in
C:
   \
src \ Metalama . Documentation \ code \ Metalama . Documentation . Helpers \ ConsoleApp \ ConsoleApp . cs :
line  22  at  Doc . StringDependencies . Program . Main (  )
at System .RuntimeMethodHandle.InvokeMethod(Object target, Void * *arguments, Signature sig, Boolean isConstructor)at System .Reflection.MethodInvoker.Invoke(Object obj, IntPtr * args, BindingFlags invokeAttr)-- - End of inner exception  stack  trace -- - at  System . Reflection . MethodInvoker . Invoke ( Object  obj , IntPtr * args, BindingFlags invokeAttr  )
at System .Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)at System .Reflection.MethodBase.Invoke(Object obj, Object[] parameters)at Metalama .Testing.AspectTesting.AspectTestRunner.ExecuteTestProgramAsync(TestInput testInput, TestResult testResult, MemoryStream peStream, MemoryStream pdbStream) in
/ _ / Metalama.Testing.AspectTesting / AspectTestRunner.cs :
line  295  using  Metalama . Patterns . Caching ;  using  Metalama . Patterns . Caching . Aspects ;  using  Metalama . Patterns . Caching . Aspects . Helpers ;  using  System ;  using  System . Collections . Generic ;  using  System . Collections . Immutable ;  using  System . Linq ;  using  System . Reflection ;
namespace Doc.StringDependencies
{
  public sealed class ProductCatalogue
  {
    private readonly Dictionary<string, decimal> _dbSimulator = new()
    {
      ["corn"] = 100
    };
    public int DbOperationCount { get; private set; }
    [Cache]
    public decimal GetPrice(string productId)
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
      }
      return _cachingService!.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice!, this, new object[] { productId }, Invoke);
    }
    private decimal GetPrice_Source(string productId)
    {
      Console.WriteLine($"Getting the price of {productId} from database.");
      this.DbOperationCount++;
      this._cachingService.AddDependency($"ProductPrice:{productId}"); /*<AddDependency>*/
      /*</AddDependency>*/
      return this._dbSimulator[productId];
    }
    [Cache]
    public string[] GetProducts()
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetProducts_Source();
      }
      return _cachingService!.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts!, this, new object[] { }, Invoke);
    }
    private string[] GetProducts_Source()
    {
      Console.WriteLine("Getting the product list from database.");
      this.DbOperationCount++;
      this._cachingService.AddDependency("ProductList");
      return this._dbSimulator.Keys.ToArray();
    }
    [Cache]
    public ImmutableDictionary<string, decimal> GetPriceList()
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetPriceList_Source();
      }
      return _cachingService!.GetFromCacheOrExecute<ImmutableDictionary<string, decimal>>(_cacheRegistration_GetPriceList!, this, new object[] { }, Invoke);
    }
    private ImmutableDictionary<string, decimal> GetPriceList_Source()
    {
      this.DbOperationCount++;
      this._cachingService.AddDependency("PriceList");
      return this._dbSimulator.ToImmutableDictionary();
    }
    public void AddProduct(string productId, decimal price)
    {
      Console.WriteLine($"Adding the product {productId}.");
      this.DbOperationCount++;
      this._dbSimulator.Add(productId, price);
      this._cachingService.Invalidate("ProductList", "PriceList");
    }
    public void UpdatePrice(string productId, decimal price)
    {
      if (!this._dbSimulator.ContainsKey(productId))
      {
        throw new KeyNotFoundException();
      }
      Console.WriteLine($"Updating the price of {productId}.");
      this.DbOperationCount++;
      this._dbSimulator[productId] = price;
      this._cachingService.Invalidate($"ProductPrice:{productId}", "PriceList"); /*<Invalidate>*/
    /*</Invalidate>*/
    }
    private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
    private static readonly CachedMethodMetadata _cacheRegistration_GetPriceList;
    private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
    private ICachingService _cachingService;
    static ProductCatalogue()
    {
      ProductCatalogue._cacheRegistration_GetPriceList = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetPriceList", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)!, "ProductCatalogue.GetPriceList()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
      ProductCatalogue._cacheRegistration_GetProducts = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)!, "ProductCatalogue.GetProducts()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
      ProductCatalogue._cacheRegistration_GetPrice = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null)!, "ProductCatalogue.GetPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    }
    public ProductCatalogue(ICachingService? cachingService = default)
    {
      this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
    }
  }
}