using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EndToEnd.DataLayer.Model
{
    /// <summary>
    /// Marker attribute to suppress auto copy
    /// </summary>
    public class SuppressPropertyCopy : Attribute
    {
    }

    public abstract class EntityBase
    {
        protected EntityBase()
        {
            CreatedAt = ModifiedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SuppressPropertyCopy]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        [SuppressPropertyCopy]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2")]
        [SuppressPropertyCopy]
        public DateTime ModifiedAt { get; set; }

        public void CopyProperties<T>(object target, params Expression<Func<T, object>>[] expressions)
          where T : EntityBase
        {
            // ,params string[] properties) {            
            var source = (T)this;
            if (expressions == null || !expressions.Any())
            {
                foreach (var piTarget in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (piTarget == null || !piTarget.CanWrite || piTarget.GetCustomAttribute(typeof(SuppressPropertyCopy), true) != null) continue;
                    var piSource = source.GetType().GetProperty(piTarget.Name);
                    var value = piSource.GetValue(source, null);
                    piTarget.SetValue(target, value, null);
                }
            }
            else
            {
                foreach (var expression in expressions)
                {
                    var exp = expression.Body;
                    string name = null;
                    if (exp.NodeType == ExpressionType.MemberAccess)
                    {
                        name = ((MemberExpression)exp).Member.Name;
                    }
                    if (exp.NodeType == ExpressionType.Convert)
                    {
                        name = ((MemberExpression)((UnaryExpression)exp).Operand).Member.Name;
                    }
                    if (String.IsNullOrEmpty(name)) continue;
                    var piSource = source.GetType().GetProperty(name);
                    var piTarget = target.GetType().GetProperty(name);
                    if (piTarget == null) continue;
                    var value = piSource.GetValue(source, null);
                    piTarget.SetValue(target, value, null);
                }
            }
        }
    }
}

