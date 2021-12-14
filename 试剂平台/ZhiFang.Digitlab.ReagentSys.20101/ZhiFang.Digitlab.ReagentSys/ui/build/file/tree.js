Ext.onReady(function(){ 
    Ext.QuickTips.init();//初始化后就会激活提示功能
    Ext.Loader.setConfig({enabled: true});//允许动态加载
    Ext.Loader.setPath('Ext.zhifangux', '../../zhifangux');
    
    var store = Ext.create('Ext.data.TreeStore',{
        fields:['text','InteractionField'],
        defaultRootProperty:'Tree',
        root:{
            text:'根节点',
            expanded:true,
            leaf:false,
            Tree:[{
                text:'节点1',
                expanded:true,
                leaf:false,
                checked:false,
                InteractionField:'L1',
                Tree:[{
                    text:'节点11',
                    expanded:true,
                    leaf:true,
                    checked:false,
                    InteractionField:'L11'
                },{
                    text:'节点12',
                    expanded:true,
                    leaf:true,
                    checked:false,
                    InteractionField:'L12'
                }]
            },{
                text:'节点2',
                expanded:true,
                leaf:true,
                checked:false,
                InteractionField:'L2'
            },{
                text:'节点3',
                expanded:true,
                leaf:false,
                checked:false,
                InteractionField:'L3',
                Tree:[{
                    text:'节点31',
                    expanded:true,
                    leaf:false,
                    checked:false,
                    InteractionField:'L31',
                    Tree:[{
                        text:'节点311',
                        expanded:true,
                        leaf:true,
                        checked:false,
                        InteractionField:'L311'
                    },{
                        text:'节点312',
                        expanded:true,
                        leaf:false,
                        checked:false,
                        InteractionField:'L312',
                        Tree:[{
                            text:'节点41',
                            expanded:true,
                            leaf:true,
                            checked:false,
                            InteractionField:'L41'
                        },{
                            text:'节点42',
                            expanded:true,
                            leaf:false,
                            checked:false,
                            InteractionField:'L42',
                            Tree:[{
                            text:'节点51',
                            expanded:true,
                            leaf:true,
                            checked:false,
                            InteractionField:'L51'
                        },{
                            text:'节点6',
                            expanded:true,
                            leaf:false,
                            checked:false,
                            InteractionField:'L52',
                            Tree:[{
	                            text:'节点61',
	                            expanded:true,
	                            leaf:true,
	                            checked:false,
	                            InteractionField:'L61'
	                        }]
                        }]
                        }]
                    }]
                },{
                    text:'节点32',
                    expanded:true,
                    leaf:true,
                    checked:false,
                    InteractionField:'L32'
                }]
            }]
        }
    });
    
    var tree = Ext.create('Ext.tree.Panel',{
        title:'树测试',
        rootVisible:false,//是否显示根节点
        defaultRootProperty:'Tree',
        store:store,
        tbar:[{text:'执行',handler:function(but){
            var p = but.ownerCt.ownerCt;
            var root = p.getRootNode();
            
            var nodes = getNodes(p);
            var a = 1;
            var PredefinedField=Ext.JSON.encode(nodes);
            alert(PredefinedField);
        }}]
    });
    
    /**
     * 获取勾选节点的立体对象
     * @private
     * @param {} tree 树面板对象
     * @return {}
     */
    var getNodes = function(tree){
        var field = 'InteractionField';//唯一字段,属性名称
        var children = 'Tree';//子节点字段
        
        //需要生成的内容
        var gatNodeInfo = function(node){
            var info = {
                expanded:true,
                text:node.get('text'),
                leaf:node.get('leaf'),
                InteractionField:node.get('InteractionField'),
                FieldClass:node.get('FieldClass')
            };
            info[children] = [];
            return info;
        };
        
        var maxLayersNumber = 0;//最大层数
        var layers = {};//层次对象
        
        //找出所有勾选的节点对象列表
        var checkedNodes = tree.getChecked();
        //循环处理每个节点
        for(var i in checkedNodes){
            //获取的结果'/Root/节点1',转化为数组:['','Root','节点1'],前面两个字符串去掉
            var path = checkedNodes[i].getPath(field);//获取节点全路径
            path = path.split('/').slice(2);
            var length = path.length;
            
            if(length == 0) continue;
            
            var node = {
                path:path,
                pathLen:length,
                node:checkedNodes[i]
            };
            if(length > maxLayersNumber){
                maxLayersNumber = length;
            }
            
            if(!layers['L'+length]){
                layers['L'+length] = [];
            }
            
            layers['L'+length].push(node);
        }
        
        //结果对象
        var nodes = {};
        nodes[children] = [];
        
        //在结果对象中获取上一级节点
        var getParentNode = function(nodeInfo){
            var node = nodeInfo.node,
                length = nodeInfo.pathLen,
                path = nodeInfo.path;
                
            var pNode = nodes;
            
            for(var i=1;i<length;i++){
                var list = pNode[children];
                for(var j in list){
                    if(list[j][field] == path[i-1]){
                        pNode = list[j];
                        break;
                    }
                }
            }
            
            return pNode;
        };
        
        //整体处理
        for(var i=1;i<=maxLayersNumber;i++){
            var list = layers['L'+i];
            for(var j in list){
                var info  = gatNodeInfo(list[j].node);
                var pNode = getParentNode(list[j]);
                pNode[children].push(info);
            }
        }
        
        return nodes;
    };
    
    
    
    //总体布局
    var viewport = Ext.create('Ext.container.Viewport',{
        layout:'fit',
        items:[tree]
    });
});