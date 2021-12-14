
　　var cm=null;
　　document.onclick = new Function("show(null)") 
function getPos(el,sProp) 
{var iPos = 0
  　　while (el!=null) 
　　 {iPos+=el["offset" + sProp]
　　　　el = el.offsetParent}
  　　return iPos} 
function show(el,m) 
{if (m) {m.style.display='';
　　　　　　m.style.pixelLeft = getPos(el,"Left")
　　　　　　m.style.pixelTop = getPos(el,"Top") + el.offsetHeight} 
  　　if ((m!=cm) && (cm)) cm.style.display='none'; cm=m } 