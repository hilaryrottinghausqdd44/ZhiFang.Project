/**
 * QMS参数设置
 * @author longfc
 * @version 2016-06-22
 */

var JcallShell = JcallShell || {};
JcallShell.System = JcallShell.System || {};

/**系统语言*/
JcallShell.System.Lang = 'CN';
/**系统信息*/
JcallShell.System.Name = '管理系统';
/**系统编号*/
JShell.System.CODE = 'QMS';
/**系统登录顶部图片*/
JcallShell.System.LoginTopImage = '/images/qms/LoginTop.jpg';

JcallShell.QMS = JcallShell.QMS || {};

/**字典类型编码*/
JcallShell.QMS.DictTypeCode = {
		/**小组*/
		SectionName: 'SectionName',
		/**仪器类型*/
		EquipType: 'MachineType',
		/**质量记录分类*/
		QRecordType: 'QRecordType'
	}
	/**枚举*/
JcallShell.QMS.Enum = {
	/**文档操作记录的操作类型*/
	FFileOperationType: {
		1: '暂存',
		2: '确认提交',
		3: '审核通过',
		4: '批准',
		5: '发布',
		//6: '浏览',
		7: '作废',
		8: '撤消提交',
		9: '审核退回',
		10: '批准退回',
		11: '撤消发布',
		12: '修订文档',
		13: '禁用',
		14: '撤消禁用',
		15: '打回起草人',
		16: '置顶',
		17: '撤消置顶',
		18: '更新文档类型',
		19: '编辑更新' //新闻管理/文档管理时的文档内容(不更新文档状态/阅读对象信息)的编辑更新操作
	},
	/**颜色*/
	FFileOperationTypeColor: {
		1: '#0D0D0D',
		2: '#FFC125',
		3: '#66CDAA',
		4: '#66CD00',
		5: '#008B00',
		6: '#FFCC00',
		7: '#EE0000',
		8: '#DC143C',
		9: '#DC143C',
		10: '#DC143C',
		11: '#DC143C',
		13: '#EE0000',
		14: '#0D0D0D',
		15: '#FF000F',
		16: '#EE0000'
	},
	/*树类型枚举**/
	FTypeTreeType: {
		'ISO151892012': '5523513825086689478',
		'qms文档类型树': '4886056329905484272',
		'智方知识库类型': '5133187353604336821',
		'实验室知识库类型': '4864154738991150328',
		'新闻频道': '5569287534789329919'
	},
	/*应用类型**/
	FType: {
		'文档应用': '1', //FFile
		'新闻应用': '2', //News
		'知识库应用': '3', //知识库
		'修订文档应用': '4' //Revise(保存到数据库时是1)
	},
	/*文档类型**/
	FFileType: {
		1: '新闻',
		2: '知识库',
		3: '质量体系'
	},
	/*应用操作分类**/
	AppOperationType: {
		'新增文档': 'AddFFile',
		'编辑文档': 'EditFFile',
		'新增新闻': 'AddNews',
		'编辑新闻': 'EditNews',
		'新增修订文档': 'AddRevise',
		'编辑修订文档': 'EditRevise'
	},

	/*文档状态**/
	FFileStatus: {
		1: '暂时存储', //起草,修订都是暂存状态
		2: '已提交',
		3: '已审核',
		4: '已批准',
		5: '发布',
		7: '作废',
		8: '撤消提交',
		9: '审核退回',
		10: '审批退回',
		//11: '撤消发布',
		15: '打回起草人'
	},

	/*文档日期类型选择**/
	FFileDateType: {
		'ffile.DrafterDateTime': '起草时间',
		'ffile.CheckerDateTime': '审核时间',
		'ffile.ApprovalDateTime': '批准时间',
		'ffile.PublisherDateTime': '发布时间'
	},
	/*树类型的分类(快捷码)**/
	FFileCopyUserType: {
		'E-1': '无',
		'E1': '全体人员',
		'E2': '按部门',
		'E3': '按角色',
		'E4': '按人员'
	},
	/*QMS角色名称映射:key为开发商代码**/
	QMSFFileRoleName: {
		'r1': '部门管理者',
		'r2': '文档审核',
		'r3': '文档审批',
		'r4': '文档发布',
		'r5': '文档浏览',
		'r6': '文档起草',
		'r7': '文档修订/作废'
	},
	/*可见级别**/
	WorkLogExportLevel: {
		'0': '仅自己和直接主管可见',
		'1': '所属部门可见',
		'2': '全公司可见'
	},
	/*程序列表状态*/
	PGMProgramStatus: {
		1: '暂时存储',
		//2: '已审核',
		3: '已发布'
	},
	/**程序列表颜色*/
	PGMProgramStatusColor: {
		1: '#0D0D0D',
		2: '#FFC125',
		3: '#66CDAA'
	},
	/*程序列表日期类型选择**/
	PGMProgramDateType: {
		'pgmprogram.DataAddTime': '加入时间',
		'pgmprogram.PublisherDateTime': '发布时间'
	},
	/**依角色的开发商代码(多个传入时为r1;r2;r3)获取员工角色的角色名称*/
	getEmployeeRoleName: function(roleName) {
		var employeeRoleName = "";
		if(roleName == null) {
			return employeeRoleName;
		}
		var list = [];
		list = roleName.split(';');
		for(var i = 0; i < list.length; i++) {
			var tempCName = JcallShell.QMS.Enum.QMSFFileRoleName[list[i]];
			if(tempCName) {
				employeeRoleName = employeeRoleName + "'" + tempCName + "'";
				if(i < list.length - 1) {
					employeeRoleName = employeeRoleName + ",";
				}
			}
		}
		return employeeRoleName;
	},

	getList: function(name, hasAll, hasColor, hasNull, isSlice, isTemporary) {
		var me = this;
		var obj = me[name];
		var list = [];

		if(!obj) return [];

		if(hasAll) {
			list.push([0, JShell.All.ALL, 'font-weight:bold;color:black;']);
		}

		for(var i in obj) {
			if(!hasNull) {
				if(obj[i] == '无') continue;
			}
			var li = [(isSlice ? i : i.slice(1)), obj[i]];

			if(hasColor) {
				li.push('font-weight:bold;color:' + me.FFileOperationTypeColor[i] + ';');
			}
			if(isTemporary) {
				if([i] != 1) {
					list.push(li);
				}
			} else {
				list.push(li);
			}
		}
		return list;
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG) {
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();