/**
 * 合同价格查询列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.SearchGrid',{
    extend:'Shell.class.pki.contractprice.EditGrid',
    title:'合同价格查询列表',
    
    /**带功能按钮栏*/
	hasButtontoolbar:true,
    /**是否带修改价格功能*/
	canEditPrice:false,
	/**导出功能*/
	buttonToolbarItems:['exp_excel'],
	/**下载EXCEL文件服务地址*/
  	downLoadExcelUrl:'/StatService.svc/Stat_UDTO_DContractPriceToExcel',
	
	/**导出EXCEL文件*/
	onExpExcelClick:function(){
		var me = this;
			
		me.doActionClick = true;
		
		var url = me.getLoadUrl();
		
		var urlArr = url.split('?');
		urlArr[0] = me.downLoadExcelUrl;
		
		url = JShell.System.Path.ROOT + urlArr.join('?');
		url += '&operateType=0';
		
		window.open(url);
	}
});