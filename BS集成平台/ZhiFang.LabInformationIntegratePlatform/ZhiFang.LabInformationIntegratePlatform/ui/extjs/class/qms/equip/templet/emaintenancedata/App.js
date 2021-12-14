/**
 * 模板日常维护
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.App', {
	extend: 'Shell.class.qms.equip.emaintenancedata.App',
	title: '质量数据',
	width: 1000,
	height: 800,
	//模板Id
	TempletID: '',
	//模板项目代码
	TempletTypeCode: '',
	selectUrl: '/QMSReport.svc/QMS_UDTO_PreviewPdf',
	//从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
    ISEDITDATE:true,
    //质量记录登记页面保存数据后是否直接预览
    IsSaveDataPreview:'0',
    hideTimes:2000,

	/***/
	createItems: function() {
		var me = this;
		var TempletPanel='Shell.class.qms.equip.templet.emaintenancedata.SimpleGrid';
		me.SimpleGrid = Ext.create(TempletPanel, {
			border: true,
			title: '模板列表',
			region: 'west',
			width: 420,
			header: false,
		    //从外边传参时间控件是否只读,默认是true，不可改, false（可改） 
            ISEDITDATE:me.ISEDITDATE,
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