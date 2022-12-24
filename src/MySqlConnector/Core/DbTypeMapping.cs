using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MySqlConnector.Core;

internal sealed class DbTypeMapping
{
	public DbTypeMapping(
#if NET5_0_OR_GREATER
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)]
#endif
		Type clrType, DbType[] dbTypes, Func<object, object>? convert = null)
	{
		ClrType = clrType;
		DbTypes = dbTypes;
		m_convert = convert;
	}

#if NET5_0_OR_GREATER
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)]
#endif
	public Type ClrType { get; }

	public DbType[] DbTypes { get; }

	public object DoConversion(object obj)
	{
		if (obj.GetType() == ClrType)
			return obj;
		return m_convert is null ? Convert.ChangeType(obj, ClrType, CultureInfo.InvariantCulture)! : m_convert(obj);
	}

	private readonly Func<object, object>? m_convert;
}
