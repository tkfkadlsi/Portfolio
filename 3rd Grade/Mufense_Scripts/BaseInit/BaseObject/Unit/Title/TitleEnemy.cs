using DG.Tweening;
using System.Collections;

public class TitleEnemy : BaseObject
{
    protected override void Init()
    {
        base.Init();

        StartCoroutine(JumpRepeat());
    }

    private IEnumerator JumpRepeat()
    {
        while (gameObject.activeInHierarchy)
        {
            transform.DOJump(transform.position, 3f, 1, 0.333f);
            yield return Managers.Instance.Game.GetWaitForSeconds(0.666f);
        }
    }
}
