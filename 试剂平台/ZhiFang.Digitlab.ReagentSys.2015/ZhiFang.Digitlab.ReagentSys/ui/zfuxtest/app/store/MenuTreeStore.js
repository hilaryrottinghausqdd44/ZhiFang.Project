//alert("MainStore.js");
Ext.define("AM.store.MenuTreeStore",{
	extend:'Ext.data.TreeStore',
	model:'AM.model.MenuTree',
    proxy: {
		//type: 'ajax',
		//url:'data/GetMenuTree.json'
		//内存方式
		type:'memory',
		data:{
			'success':true,
			'errorInfo':'',
			'tree':[
            {'text':'日期控件和时间控件','url':'/LabStarLIMS/ui/zfuxtest/DateTime.html','expanded':true,'leaf':true},
            {'text':'单选组项组件','url':'/LabStarLIMS/ui/zfuxtest/UXRadioGroup.html','expanded':true,'leaf':true},
            {'text':'多选组项(岗位选择)组件','url':'/LabStarLIMS/ui/zfuxtest/panelcheckboxgroup.html','expanded':true,'leaf':true},
            {'text':'树节点移动','url':'/LabStarLIMS/ui/zfuxtest/tree/treepanel.html','expanded':true,'leaf':true},
            {'text':'树的多选组件','url':'/LabStarLIMS/ui/zfuxtest/UXMorecheckTree.html','expanded':true,'leaf':true},
            {'text':'文字属性配置组件','url':'/LabStarLIMS/ui/zfuxtest/fontstyleset.html','expanded':true,'leaf':true},    
            {'text':'双表数据移动组件','url':'/LabStarLIMS/ui/zfuxtest/DdItems.html','expanded':true,'leaf':false,'tree':[    
                {'text':'不带复选框','url':'/LabStarLIMS/ui/zfuxtest/danbiao.html','expanded':true,'leaf':true},
                {'text':'带复选框','url':'/LabStarLIMS/ui/zfuxtest/fuxuan.html','expanded':true,'leaf':true}
            ]},      
            {'text':'列表排序组件','url':'/LabStarLIMS/ui/zfuxtest/SortList.html','expanded':true,'leaf':true},
            {'text':'多项选择拖动','url':'/LabStarLIMS/ui/zfuxtest/multiplechoice.html','expanded':true,'leaf':false,'tree':[    
                {'text':'不带复选框','url':'/LabStarLIMS/ui/zfuxtest/dxdanxuan.html','expanded':true,'leaf':true},
                {'text':'带复选框','url':'/LabStarLIMS/ui/zfuxtest/dxduoxuan.html','expanded':true,'leaf':true}
            ]},
            {'text':'部门人员选择器组件','expanded':true,'leaf':false,'tree':[    
                {'text':'单个人员选择器','url':'/LabStarLIMS/ui/zfuxtest/peoplePickerSingle.html','expanded':true,'leaf':true},
                {'text':'多个人员选择器','url':'/LabStarLIMS/ui/zfuxtest/peoplePickerMul.html','expanded':true,'leaf':true}
            ]},
            {'text':'部门选择器','url':'/LabStarLIMS/ui/zfuxtest/DepSelector.html','expanded':true,'leaf':true},
            {'text':'部门下拉树组件','url':'/LabStarLIMS/ui/zfuxtest/UXComboboxTree.html','expanded':true,'leaf':true},
            {'text':'行数据条件组件','url':'/LabStarLIMS/ui/zfuxtest/RowsDataSet.html','expanded':true,'leaf':true},
            {'text':'列数据条件组件','url':'/LabStarLIMS/ui/zfuxtest/ColumnCondition.html','expanded':true,'leaf':true},
            {'text':'图标状态集合显示组件','url':'/LabStarLIMS/ui/zfuxtest/Status.html','expanded':true,'leaf':true}
			]
		}
    },
    defaultRootProperty: 'tree',//子节点的属性名
    root: {
		text: '功能菜单',
		leaf: false,
		expanded: true,
		url: 'about.html'
	}
});