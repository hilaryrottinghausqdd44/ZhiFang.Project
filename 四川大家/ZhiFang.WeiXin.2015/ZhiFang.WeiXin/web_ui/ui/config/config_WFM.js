/**
 * WFM参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};
JcallShell.System = JcallShell.System || {};

/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = '管理系统';
/**系统编号*/
JcallShell.System.CODE = 'WFM';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/wfm/LoginTop.jpg';

JcallShell.WFM = JcallShell.WFM || {};

/**字典类型编码*/
JcallShell.WFM.DictTypeCode = {
	/**任务类型*/
	TaskType:'TaskType',
	/**执行方式*/
	TaskExecutType:'ExecutType',
	/**任务状态*/
	TaskStatus:'Status',
	/**任务进度*/
	TaskPace:'Pace',

	/**协作评估*/
	TeamworkEval:'TeamworkEval',
	/**进度评估*/
	PaceEval:'PaceEval',
	/**效率评估*/
	EfficiencyEval:'EfficiencyEval',
	/**质量评估*/
	QualityEval:'QualityEval',
	/**总体评估*/
	TotalityEval:'TotalityEval',

	/**紧急程度*/
	Urgency:'Urgency',

	/**项目类型*/
	ProjectType:'ProjectType',
	/**项目状态*/
	ProjectStatus:'ProjectStatus',
	/**项目进度*/
	ProjectPace:'ProjectPace',
	/**文档内容类型*/
	FFileContentType:'FFileContentType'
},
/**字典编码*/
JcallShell.WFM.DictCode = {
	/**任务状态*/
	TaskStatus:{
		'E1':{value:'1',text:'申请',className:'label-warning',GUID:'4649883000533627142'},
		'E6':{value:'6',text:'待执行',className:'label-primary',GUID:'4967258396876635439'},
		'E2':{value:'2',text:'执行中',className:'label-success',GUID:'4621621720762238176'},
		'E3':{value:'3',text:'已完成',className:'label-warning',GUID:'5391772382920326538'},
		'E4':{value:'4',text:'已终止',className:'label-default',GUID:'5484216973649314900'},
		'E5':{value:'5',text:'不执行',className:'label-success',GUID:'5001555516032423353'},
		'E7':{value:'7',text:'已验收',className:'label-success',GUID:'5518558271903118484'}
	},
	/**紧急程度*/
	TaskUrgency:{
		'EJJZY':{value:'EJJZY',text:'紧急重要',className:'label-danger'},
		'EJJBZY':{value:'EJJBZY',text:'紧急不重要',className:'label-warning'},
		'EBJJZY':{value:'EBJJZY',text:'不紧急重要',className:'label-primary'},
		'EBJJBZY':{value:'BJJBZY',text:'不紧急不重要',className:'label-info'}
	}
}

/**GUID码*/
JcallShell.WFM.GUID = {
	/**字典类型*/
	DictType:{
		/**医院类别*/
		HospitalType:{text:'医院类别',value:'4857993353299957174'},
		/**医院等级*/
		HospitalLevel:{text:'医院等级',value:'5651076454187258623'},
		/**客户类型*/
		ClientType:{text:'客户类型',value:'5656484832236139054'},
		/**地理区域*/
		ClientArea:{text:'地理区域',value:'5408047518060539453'}
	},
	/**角色*/
	Role:{
		/**任务二审*/
		TaskExamineTwo:{text:'任务二审',GUID:'5319166555730457687'},
		/**任务分配*/
		TaskAllot:{text:'任务分配',GUID:'5179670536727949932'},
		/**销售人员*/
		Saler:{text:'销售人员',GUID:'5703185968099922266'}
	},
	DictTree:{
		/**任务类型*/
		TaskType:{text:'任务类型',GUID:'5753060783994672008'}
	},
	/**任务状态*/
	TaskStatus:{
		Temporary:{text:'暂存',GUID:'5588205522535239744',bgcolor:'#bfbfbf',color:'#ffffff'},
		Apply:{text:'申请',GUID:'4649883000533627142',bgcolor:'#f4c600',color:'#ffffff'},
		OneAuditIng:{text:'一审中',GUID:'5097561283722347016',bgcolor:'#aad08f',color:'#ffffff'},
		OneAuditOver:{text:'一审通过',GUID:'5682026874588216259',bgcolor:'#7cba59',color:'#ffffff'},
		OneAuditBack:{text:'一审退回',GUID:'5150044114524428979',bgcolor:'#2aa515',color:'#ffffff'},
		TwoAuditIng:{text:'二审中',GUID:'5221836557183432811',bgcolor:'#7dc5eb',color:'#ffffff'},
		TwoAuditOver:{text:'二审通过',GUID:'5434763342795916000',bgcolor:'#17abe3',color:'#ffffff'},
		TwoAuditBack:{text:'二审退回',GUID:'5324460690574315774',bgcolor:'#1195db',color:'#ffffff'},
		PublisherIng:{text:'分配中',GUID:'5181783138154880332',bgcolor:'#be8dbd',color:'#ffffff'},
		PublisherOver:{text:'分配完成',GUID:'4967258396876635439',bgcolor:'#a4579d',color:'#ffffff'},
		PublisherBack:{text:'分配退回',GUID:'5741016721777107562',bgcolor:'#88147f',color:'#ffffff'},
		ExecuteIng:{text:'执行中',GUID:'4621621720762238176',bgcolor:'#e8989a',color:'#ffffff'},
		ExecuteOver:{text:'执行完成',GUID:'5391772382920326538',bgcolor:'#dd6572',color:'#ffffff'},
		NoExecute:{text:'不执行',GUID:'5001555516032423353',bgcolor:'#d6204b',color:'#ffffff'},
		CheckIng:{text:'验收中',GUID:'4890928177429564879',bgcolor:'#eeb173',color:'#ffffff'},
		CheckOver:{text:'已验收',GUID:'5518558271903118484',bgcolor:'#e98f36',color:'#ffffff'},
		CheckBack:{text:'验收退回',GUID:'5490921315541028028',bgcolor:'#e0620d',color:'#ffffff'},
		IsStop:{text:'已终止',GUID:'5484216973649314900',bgcolor:'#2c2c2c',color:'#ffffff'}
	},
	/**任务状态*/
	TaskUrgency:{
		EJJZY:{text:'紧急重要',GUID:'101',bgcolor:'#d81e06',color:'#ffffff'},
		EJJBZY:{text:'紧急不重要',GUID:'102',bgcolor:'#ea8010',color:'#ffffff'},
		EBJJZY:{text:'不紧急重要',GUID:'103',bgcolor:'#ea8010',color:'#ffffff'},
		EBJJBZY:{text:'不紧急不重要',GUID:'104',bgcolor:'#11cd6e',color:'#ffffff'}
	},
	/**根据GUID获取信息*/
	getInfoByGUID:function(name,value){
		var obj = this[name];
		if(!obj) return null;
		
		for(var i in obj){
			if(obj[i].GUID == value){
				return Ext.clone(obj[i]);
			}
		}
		
		return null;
	},
	/**获取基础列表*/
	getSimpleList:function(name){
		var obj = this[name];
		if(!obj) return [];
		
		var list = [];
		for(var i in obj){
			list.push([obj[i].GUID,obj[i].text]);
		}
		
		return list;
	}
}

/**枚举*/
JcallShell.WFM.Enum = {
	/**单位类型*/
	UnitType: {
		'E0': '无',
		'E1': '送检单位',
		'E2': '经销商',
		'E3': '个人'
	},
	/**颜色*/
	Color: {
		'E0': '#FFCC00',
		'E1': '#FF99CC',
		'E2': '#99CC33',
		'E3': '#CC0033',
		'E4': '#663366',
		'E5': '#999966',
		'E6': '#663300',
		'E7': '#6699CC'
	},
	/**
	 * @param {Boolean} name 枚举类型名称
	 * @param {Boolean} hasAll 是否带'全部'选项
	 * @param {Boolean} hasColor 是否带颜色属性
	 * @param {Boolean} hasNull 是否带'无'选项
	 */
	getList: function(name, hasAll, hasColor,hasNull) {
		var me = this;
		var obj = me[name];
		var list = [];

		if (!obj) return [];

		if (hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for (var i in obj) {
			if(!hasNull){
				if(obj[i] == '无') continue;
			}
			var li = [i.slice(1), obj[i]];
			if (hasColor) {
				li.push('font-weight:bold;color:' + me.Color[i] + ';');
			}
			list.push(li);
		}

		return list;
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG){
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();