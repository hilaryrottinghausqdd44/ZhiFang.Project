<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeUC.ascx.cs" Inherits="TreeItem.TreeUC" %>
 <asp:XmlDataSource ID="XmlDataSource1" runat="server"></asp:XmlDataSource>
    <script type="text/javascript" language="javascript">
      var vflag = false;
  //识别不同的浏览器 
      function getTargetElement(evt) { 
        var elem 
        if (evt.target) 
        { 
          elem = (evt.target.nodeType == 3) ? evt.target.parentNode : evt.target 
        }  
        else  
        { 
          elem = evt.srcElement 
        } 
        return elem 
      } 

    //去掉字符串左右空格
      String.prototype.Trim = function() 
     { 
        return this.replace(/(^\s*)|(\s*$)/g, ""); 
     }
     
    function OnClientTreeNodeClick(evt) 
    {
        evt = (evt) ? evt : ((window.event) ? window.event : ""); 
        if(evt == "") 
        { 
           return; 
        } 
        var obj = getTargetElement(evt); 
        if(obj.tagName) 
        {  
           var TreeView = "<%=tvMenu.ClientID %>" ;//取得TREEVIEW的客户端ID
           //alert(obj.tagName+obj.id.substr(0,TreeView.length));
           //判断当前选中的节点是以A开头的元素
            if(obj.tagName == "A" && obj.id.substr(0,TreeView.length) == TreeView) 
           {  
                 //点击有子节点的父节点时不展开或收缩节点 开始
                 if(obj.href)
                 {
                    obj.href = "javascript:void(0)";
                 }
                 //点击有子节点的父节点时不展开或收缩节点 结束            
                //新加对当前选择的节点字体加粗开始
                if(vflag == false)
                {
                    //第一次单击 记录上次点击的记录 下一次时将上次的记录复原
                    if(document.getElementById('treenodeid').value.length > 0)
                    {
                        var tmpid = document.getElementById('treenodeid').value;
                        document.getElementById(tmpid).innerHTML = document.getElementById('treenodevalue').value;
                     }   
                     document.getElementById('treenodeid').value = obj.id;
                     document.getElementById('treenodevalue').value = obj.innerHTML;                   
                     vflag = true;                    
                }
                else
                {
                    //复原上次记录 并记录当前点击的节点记录
                    var tmpid = document.getElementById('treenodeid').value;
                    document.getElementById(tmpid).innerHTML = document.getElementById('treenodevalue').value;
                    
                    document.getElementById('treenodeid').value = obj.id;
                    document.getElementById('treenodevalue').value = obj.innerHTML;
                    vflag = false;
                }
                obj.innerHTML = "<B><U>"+obj.innerText+"</U></B>";
                //新加对当前选择的节点字体加粗结束
                
                //当此元素不等于table时找到该元素的父节点            
                while (obj.tagName != "TABLE") 
                { 
                  obj = obj.parentNode; 
                } 
                //父节点为table 循环行
                for(var i = 0;i<obj.rows.length;i++)
                {
                   //取到每行的列数
                   var parentTreeDeep = obj.rows[i].cells.length; 
                   //当列数大于3时才存在所要找的值
                    if(parentTreeDeep >= 3)
                    {     
                        //alert(i); 
                        var parentTreeNode = "";
                        //alert('obj.rows[i].cells.length:'+parentTreeDeep);
                        //循环列
                        for(var j=0;j<parentTreeDeep;j++)
                        {   //alert(j);
                            //得到该列的对象
                            parentTreeNode = obj.rows[i].cells[j];   
                            //取得该列下的所有img对象                         
                            var imgtag1 = parentTreeNode.getElementsByTagName("IMG");
                            //alert('imgtag1.length:'+imgtag1.length); 
                            //判断img对象是否有效                          
                            if(imgtag1.length == 1)
                            { 
                                //取得第一个img上的alt值
                                var pams1 = imgtag1[0].alt;             
                                if(pams1)
                                {
                                  //判断是否包含有@字符, 此值是在后台设好的
                                  if(pams1.indexOf('@') > 0)
                                  {    //alert(pams1);
                                      var pamlist = new Array;
                                      pamlist = pams1.split("@");
                                      //调用要刷新的方法
                                      GetTreeNodeIdAndName(pamlist[0],pamlist[1],pamlist[2],pamlist[3]);
                                   }
                                }
                            }
                        }                       
                     }                    
                }               
            }
        } 
     } 
   </script>
   <input type="hidden" id="treenodeid" />
   <input type="hidden" id="treenodevalue" />
 <asp:TreeView ID="tvMenu" runat="server" Width="100%" ImageSet="Arrows" 
    ShowLines="True" ExpandDepth="0" Target="middle">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Names="Tahoma"  Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
                <DataBindings>
                    <asp:TreeNodeBinding DataMember="treenode" ImageUrlField="ImageUrl" TargetField="NavigateUrl" TextField="Text" ToolTipField="Value" />
                </DataBindings>
        </asp:TreeView>