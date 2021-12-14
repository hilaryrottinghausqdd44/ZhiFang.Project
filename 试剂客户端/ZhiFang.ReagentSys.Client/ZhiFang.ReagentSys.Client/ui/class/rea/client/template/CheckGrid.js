/**
 * 模板选择列表
 * 获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息
 * @author longfc
 * @version 2018-08-21
 */
Ext.define('Shell.class.rea.client.template.CheckGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	title: '模板选择列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 480,
	height: 420,
	/**是否单选*/
	checkOne: false,
	/**是否带清除按钮*/
	hasClearButton: true,
	/**后台排序*/
	remoteSort: false,

	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: "1",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "",

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchBTemplateByLabIdAndType',

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
			dataIndex: 'CName',
			sortable: true,
			text: '模板名称',
			width: 120,
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
	initButtonToolbarItems: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		if(me.hasClearButton) {
			me.buttonToolbarItems.unshift({
				text: '清除',
				iconCls: 'button-cancel',
				tooltip: '<b>清除原先的选择</b>',
				handler: function() {
					me.fireEvent('accept', me, null);
				}
			});
		}
		if(me.hasAcceptButton) {
			me.buttonToolbarItems.push('->', 'accept');
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		var labId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID);
		url += (url.indexOf('?') == -1 ? '?' : '&') + "labId=" + labId + "&breportType=" + me.breportType + "&publicTemplateDir=" + JShell.String.encode(me.publicTemplateDir);
		return url;
	}
});