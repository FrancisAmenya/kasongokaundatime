using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    public IEnumerator ScaleUp()
    {

        transform.localScale = transform.localScale * .95f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale / .95f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale * 1.05f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale * 1.05f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale / 1.05f;

        yield return new WaitForSeconds(.04f);

    }


    public IEnumerator ScaleDown()
    {

        transform.localScale = transform.localScale * 1.05f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale / 1.05f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale / 1.05f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale * .95f;

        yield return new WaitForSeconds(.04f);

        transform.localScale = transform.localScale / .95f;

        yield return new WaitForSeconds(.04f);

    }

}
