using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;


namespace GenPres.Business.Data
{
    public class DataKeyMap
    {
        public PropertyInfo DataKeyProperty { get; set; }
        public PropertyInfo ObjectKeyProperty { get; set; }

        public static DataKeyMap GetKeyMap(Type type, Expression sourceMember, Expression destinationMember, System.Data.Linq.DataContext dataContext)
        {
            DataKeyMap dataKeyMap = new DataKeyMap();
            MemberExpression srcMemberExpr = null;
            MemberExpression destMemberExpr = null;

            PropertyInfo srcPropInfo;

            if (sourceMember.NodeType == ExpressionType.Convert)
            {
                Type pType = sourceMember.Type;

                System.Data.Linq.Mapping.MetaTable mapping = dataContext.Mapping.GetTable(type);

                MemberInfo b = (from a in mapping.RowType.Associations
                                where
                                   a.ThisKey.Single<System.Data.Linq.Mapping.MetaDataMember>().Name == ((MemberExpression)((UnaryExpression)sourceMember).Operand).Member.Name
                                select a.ThisMember.Member).FirstOrDefault<MemberInfo>();
                srcPropInfo = b as PropertyInfo;
            }
            else
            {
                srcMemberExpr = sourceMember as MemberExpression;
                srcPropInfo = srcMemberExpr.Member as PropertyInfo;
            }
            if (destinationMember.NodeType == ExpressionType.MemberAccess)
            {
                destMemberExpr = destinationMember as MemberExpression;
                PropertyInfo destPropInfo = destMemberExpr.Member as PropertyInfo;
                dataKeyMap.ObjectKeyProperty = destPropInfo;
            }
            dataKeyMap.DataKeyProperty = srcPropInfo;
            return dataKeyMap;   
        }
    }
}
