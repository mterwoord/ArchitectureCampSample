using EndToEnd.DataLayer.Context;
using System;

namespace EndToEnd.BusinessLayer {

  /// <summary>
  /// Base class for all BLL classes
  /// </summary>
  public abstract class Repository<T> : Singleton<T>, IRepository, IDisposable where T : new() {

    private IUnitOfWork _ctx;

    protected Repository() {

    }

    public Repository(IUnitOfWork unitOfWork) {
      Ctx = unitOfWork as EndToEndContext;
    }

    public EndToEndContext Ctx {
      get {
        return (_ctx ?? UnitOfWorkFactory.GetIUnitOfWorkContext<EndToEndContext>()) as EndToEndContext;
      }
      private set {
        _ctx = value;
      }
    }

    public IUnitOfWork AsUnitOfWork() {
      return Ctx;
    }

    public int SaveChanges() {
      return Ctx.Save();
    }

    public void UndoChanges() {
      Ctx.UndoChanges();
    }

    public void Dispose() {
    }
  }
}
