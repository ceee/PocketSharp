

(function ()
{
  var anchors = document.querySelectorAll('.app-nav a');

  if (window.location.hash.length > 1)
  {
    console.info(window.location.hash);
    changePart.call(document.querySelector('a[data-id="' + window.location.hash.slice(1) + '"]'));
  }


  for (var i = anchors.length - 1; i >= 0; i--)
  {
    anchors[i].addEventListener('click', changePart, false);
  }


  function changePart(e)
  {
    
    if (e)
    {
      e.preventDefault();
    }

    if (!this.getAttribute)
    {
      return;
    }

    var id = this.getAttribute('data-id');
    var parts = document.querySelectorAll('div[data-part]');
    var isActive = false;

    for (var i = parts.length - 1; i >= 0; i--)
    {
      isActive = parts[i].getAttribute("data-part") === id;

      parts[i].style.display = isActive ? "block" : "none";

      changeActiveClass(this);
    }

    window.location.hash = id;
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