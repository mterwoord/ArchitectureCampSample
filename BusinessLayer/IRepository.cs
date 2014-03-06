using EndToEnd.DataLayer.Context;
using System;

namespace EndToEnd.BusinessLayer {

  /// <summary>
  /// Unified structure of business layer entities.
  /// </summary>
  public interface IRepository {
    /// <summary>
    /// Allows the caller to override the internal context. This is used internally for setup. Should remain <c>null</c>.
    /// </summary>
    EndToEndContext Ctx { get; }
    /// <summary>
    /// Convenient access to Context.SaveChanges with error handling and logging.
    /// </summary>
    /// <returns></returns>
    int SaveChanges();
  }
}
