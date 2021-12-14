/**
 * 服务器程序授权明细
 * @author longfc
 * @version 2016-10-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.apply.ProgramLicenceGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ProgramLicenceGrid',
	title: '服务器程序授权明细',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerProgramLicenceByHQL',
	/**修改服务地址*/
	/**上传的授权申请文件的程序授权明细信息*/
	ApplyProgramInfoList: null,
	/**后台排序*/
	remoteSort: true,
	/**默认加载*/
	defaultLoad: false
});