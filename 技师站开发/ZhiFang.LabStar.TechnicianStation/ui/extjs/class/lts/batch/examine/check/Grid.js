/**
 *检验单
 * @author liangyl
 * @version 2021-03-26
 */
Ext.define('Shell.class.lts.batch.examine.check.Grid',{
    extend:'Shell.class.lts.batch.examine.basic.Grid',
   
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormBySampleNo?isPlanish=true',
	
	//查询条件
	whereObj:{
        SickTypeID:null,//就诊类型
        StartDate:"",//检验日期开始日期
        EndDate:"",//检验日期结束日期
        DeptID:null,//送检科室
        beginSampleNo:"",//开始样本号        
        endSampleNo:"", //结束样本号
        MainStatusID:'' //全部—检验初审完成
	},

	defaultWhere :'listestform.MainStatusID =2',

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		//样本号范围
		if(me.whereObj.beginSampleNo)url+='&beginSampleNo='+me.whereObj.beginSampleNo;
		if(me.whereObj.endSampleNo)url+='&endSampleNo='+me.whereObj.endSampleNo;
		//小组ID
		if(me.SectionID) {
			params.push("listestform.LBSection.Id=" + me.SectionID + "");
		}
		if(me.whereObj.StartDate)params.push("listestform.GTestDate>='" + me.whereObj.StartDate + "'");
   		if(me.whereObj.EndDate)params.push("listestform.GTestDate<='" + me.whereObj.EndDate + "'");
        //科室
        if(me.whereObj.DeptID)params.push("listestform.DeptID=" + me.whereObj.DeptID + "");
        //就诊类型
        if(me.whereObj.SickTypeID)params.push("listestform.SickTypeID=" + me.whereObj.SickTypeID + "");

        if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		var where = me.getLoadWhere(true);
		if (where) {
			url += '&where=' + where;
		}
		return url;
	}
});