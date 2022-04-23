using System;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    #region Base Scene's Base Properties
    [field: Header("Scene's Base Data")]
    //[field: SerializeField] protected Transform         canvas      { get; private set; }

    [field: SerializeField] protected SpriteRenderer    backgroundRenderer { get; private set; }
    [field: SerializeField] protected Animator          backgroundAnim  { get; private set; }
    #endregion

    #region Unity Messages
    private void Awake()
    {
        try
        {
            var background = transform.Find("Background");

            backgroundRenderer  = background.GetComponent<SpriteRenderer>();
            backgroundAnim      = background.GetComponent<Animator>();
        } 
        
        catch (NullReferenceException ex)
        {
            return;
        } 
        
        finally
        {
            InitSceneData();
        }
    }

    private void Start()
    {
        //if (!(this is LoadingScene))
        //{
        //    SoundManager.GetInstance().PlayBGM($"BGM_{GetType()}", true);
        //}
        //
        //else
        //{
        //    SoundManager.GetInstance().StopBGM();
        //}

        OnStartedScene();
    }

    private void Update()
    {
        OnUpdatedScene();
    }
    #endregion

    #region Base Scene's Methods
    protected abstract void InitSceneData();
    protected abstract void OnStartedScene();
    protected abstract void OnUpdatedScene();
    #endregion
}
