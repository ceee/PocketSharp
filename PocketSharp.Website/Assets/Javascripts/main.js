

(function ()
{
  var anchors = document.querySelectorAll('.app-nav a');

  for (var i = anchors.length - 1; i >= 0; i--)
  {
    anchors[i].addEventListener('click', changePart, false);
  }



  function changePart(e)
  {
    e.preventDefault();
 
    var id = this.id;
    var parts = document.querySelectorAll('div[data-part]');
    var isActive = false;

    for (var i = parts.length - 1; i >= 0; i--)
    {
      isActive = parts[i].getAttribute("data-part") === id;

      parts[i].style.display = isActive ? "block" : "none";

      changeActiveClass(this);
    }
  }


  function changeActiveClass(el)
  {
    for (var i = anchors.length - 1; i >= 0; i--)
    {
      anchors[i].classList.remove('is-active');
    }
    el.classList.add('is-active');
  }

})();