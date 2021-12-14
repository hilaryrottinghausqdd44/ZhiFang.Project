/**
   @Name：小组项目选择
   @Author：liangyl
   @version 2019-05-30
 */
layui.extend({
	uxutil: 'ux/util',
    sortlist:'app/dic/section/item/sort/sortlist'
}).use(['sortlist','layer','uxutil'],function(){
	var $ = layui.$,
	    uxutil = layui.uxutil,
		sortlist = layui.sortlist;

    sortlist.render({
        elemDiv : "#divContent",
        //显示列前缀
        prefix:'LBSectionItemVO_LBSectionItem',
        //显示列
        ordercols:[
			{field:'LBSectionItemVO_LBItem_Id',width: 180,title: '项目编号',hide:true},
	        {field:'LBSectionItemVO_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
			{field:'LBSectionItemVO_LBItem_EName', width:100,title: '英文名称', sort: true},
			{field:'LBSectionItemVO_LBItem_SName', width:100,title: '简称', sort: true},
			{field:'LBSectionItemVO_LBItem_DispOrder', width:100,title: '项目排序', sort: true}
	    ],
	    pkfield:'LBSectionItemVO_LBSectionItem_Id',//主键
	    disporderfield:'LBSectionItemVO_LBSectionItem_DispOrder',//次序
	    //获取小组列表服务地址
	    selectUrl :uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?isPlanish=true',
	    //修改小组项目服务地址
	    editUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionItemByField',
	    elem:'#sortList',
	    id:'sortList',
	    title:'小组项目排序',
	    height:'full-40',
	    size: 'sm', //小尺寸的表格
	    //defaultOrderBy:[{property: 'LBSectionItem_DispOrder',direction: 'ASC'},{property: 'LBItem_DispOrder',direction: 'ASC'}],
		//initSort: {field: 'LBSectionItemVO_LBSectionItem_DispOrder',type: 'asc' },
	    where:{
	    	where:'lbsection.Id='+document.getElementById("sectionID").value,
	    	sort:JSON.stringify([
	    		{property: 'LBSectionItem_DispOrder',direction: 'ASC'},
	    		{property: 'LBItem_DispOrder',direction: 'ASC'}
	    	])
	    }
    });
});