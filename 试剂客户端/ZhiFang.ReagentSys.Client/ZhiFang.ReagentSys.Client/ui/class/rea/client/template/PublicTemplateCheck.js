/**
 * 公共模板选择列表
 * @author longfc
 * @version 2018-08-21
 */
Ext.define('Shell.class.rea.client.template.PublicTemplateCheck', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '公共模板选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 480,
	height: 420,
	/**是否单选*/
	checkOne: false,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**后台排序*/
	remoteSort: false,
	//searchInfo: null,
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "",
	
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchPublicTemplateFileInfoByType',
	
	initComponent: function() {
		var me = this;
		//JShell.REA.StatusList.getStatusList(me.BTemplateType, false, true, null);
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BTemplateType',
			text: '模板类型',
			sortable: true,
			minWidth: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'CName',
			sortable: true,
			text: '模板名称',
			width: 280,
			defaultRenderer: true
		}, {
			dataIndex: 'FileName',
			text: '文件名称',
			hidden: true
		}, {
			dataIndex: 'FullPath',
			text: '主键ID',
			hidden: true,
			//hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :JShell.System.Path.ROOT) + me.selectUrl;
		//var labId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID);		
		url+=(url.indexOf('?') == -1 ? '?' : '&') +"publicTemplateDir="+JShell.String.encode(me.publicTemplateDir);
		return url;
	}
});