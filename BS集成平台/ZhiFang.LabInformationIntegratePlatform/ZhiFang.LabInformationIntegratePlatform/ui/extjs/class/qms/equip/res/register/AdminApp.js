/**
 * 质量数据登记（职责)
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.res.register.AdminApp', {
	extend: 'Shell.class.qms.equip.emaintenancedata.App',
	title: '质量数据',
//	layout:'border',
	width: 1000,
	height: 800,
	//模板Id
	TempletID: '',
	//模板项目代码
	TempletTypeCode: '',
	selectUrl: '/QMSReport.svc/QMS_UDTO_PreviewPdf',
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:'false',
    /**'0', '全部','1', '人员模板','2', '人员岗位模板',对外公开设置默认值*/
    TEMPTLETTYPE:'0',
    IsSaveDataPreview:'0',
    hideTimes:2000,
	/***/
	createItems: function() {
		var me = this;
		var TempletPanel='Shell.class.qms.equip.res.register.TempletGrid';
		me.SimpleGrid = Ext.create(TempletPanel, {
			border: true,
			title: '模板列表',
			region: 'west',
			width: 420,
			header: false,
		    //从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
            ISEDITDATE:me.ISEDITDATE,
            TEMPTLETTYPE:me.TEMPTLETTYPE,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			name: 'SimpleGrid',
			itemId: 'SimpleGrid'
		});
		me.ShowPanel = Ext.create('Shell.class.qms.equip.emaintenancedata.ShowPanel', {
			border: true,
			title: '操作列表',
			region: 'center',
			header: false,
			ISEDITDATE:me.ISEDITDATE,
			itemId: 'ShowPanel'
		});
	   
		return [me.SimpleGrid, me.ShowPanel];
	}
});