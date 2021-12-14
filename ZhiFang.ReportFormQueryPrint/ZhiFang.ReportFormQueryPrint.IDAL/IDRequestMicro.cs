using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// �ӿڲ�IRequestMicro ��ժҪ˵����
	/// </summary>
    public interface IDRequestMicro : IDataBase<Model.RequestMicro>
	{
        /// <summary>
        /// ����FormNo����ReportForm������RequestMicro�б�
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetRequestMicroList(string FormNo);

        DataTable GetRequestMicroGroupListForSTestType(string FormNo);

        /// <summary>
        /// ����FormNo����ReportForm������RequestItem�б�(xsltģ��ʹ��)
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetRequestMicroGroupList(string FormNo);
    }
}
