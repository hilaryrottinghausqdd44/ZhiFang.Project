/**
 *检验单
 * @author liangyl
 * @version 2021-03-26
 */
Ext.define('Shell.class.lts.batch.examine.confirm.Grid',{
    extend:'Shell.class.lts.batch.examine.basic.Grid',
   
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryWillConfirmLisTestForm?isPlanish=true',
	
	//查询条件
	whereObj:{
    	ZFSysCheckStatus:false,//全部——智能审核成功样本
        TestStatus:false,//全部——检验完成样本
        SickTypeID:null,//就诊类型
        StartDate:"",//检验日期开始日期
        EndDate:"",//检验日期结束日期
        DeptID:null,//送检科室
        beginSampleNo:"",//开始样本号        
        endSampleNo:"",//结束样本号
        itemResultFlag:""//无阳性,无异常,无HH/LL
	},

	defaultWhere :'listestform.MainStatusID=0',

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		//样本号范围
		if(me.whereObj.beginSampleNo)url+='&beginSampleNo='+me.whereObj.beginSampleNo;
		if(me.whereObj.endSampleNo)url+='&endSampleNo='+me.whereObj.endSampleNo;
		url+='&itemResultFlag='+me.whereObj.itemResultFlag;
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
        //全部—智能审核成功样本
        if(me.whereObj.ZFSysCheckStatus)params.push("listestform.ZFSysCheckStatus=1");
        //全部—检验完成样本
        if(me.whereObj.TestStatus)params.push("(listestform.TestAllStatus=1 and listestform.FormInfoStatus=1)");

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