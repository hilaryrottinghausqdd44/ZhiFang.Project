/**
 * 定性定量结果
 * @author Jcall
 * @version 20200327
 */
Ext.define('Shell.class.lts.sample.result.history.result.Normal',{
    extend:'Ext.panel.Panel',
    title:'定性定量结果',
    
    //定性结果外部地址
    LabStartUrl:'/ZhiFang.ReportFormQueryPrint/ui_new/class/labStar/projectContrast/index.html',
    //是否加载过数据
	hasLoaded:false,
    //查询条件对象
    searchParams:null,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	//查询数据
	onSearch:function(searchParams){
		var me = this;
		me.searchParams = searchParams;
		
		if(!me.hasLoaded){
			me.hasLoaded = true;
		}
		
		var where = searchParams.where;
		if(where == null || where == ""){
			me.body.update('<div style="text-align:center;padding:50px;color:red;">传入条件不能为空！</div>');
			return;
		}
		var reporturl = JcallShell.System.Path.LOCAL + me.LabStartUrl + "?where=" + where;
		
		me.body.update('<iframe src="' + reporturl + '" style="border: medium none;height:100%;width:100%;"></iframe>');
	},
	//清空数据
	clearData:function(){
		this.body.update('<div style="text-align:center;padding:50px;color:red;">检验信息不存在，无法查询！</div>');
	}
});