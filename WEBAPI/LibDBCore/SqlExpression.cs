using System;
using System.Collections.Generic;
using System.Text;

namespace Db_Core
{
    public class SqlExpression
    {
        private String expr;

        private SqlExpression(string expr) {
            this.expr = expr;
        }

        public override string ToString()
        {
            return expr;
        }

        public static SqlExpression From(string expr)
        {
            return new SqlExpression(expr);
        }
    }
}
