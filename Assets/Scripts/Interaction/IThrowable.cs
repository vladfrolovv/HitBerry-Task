using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable {
  // in case we want to add some trap objects (like rock) which we can throw, but can't blend
  public GameObject Throw();
}
