using UnityEngine;

abstract public class Complex : MonoBehaviour
{
    protected TextMesh _textMesh;
    protected TextMesh textMesh
    {
        get
        {
            if (this._textMesh == default(TextMesh))
            {
                this._textMesh = this.GetFirstComponent<TextMesh>(this.transform);
            }

            return this._textMesh;
        }
    }

    protected MeshRenderer _renderer;
    protected MeshRenderer renderer
    {
        get
        {
            if (this._renderer == default(MeshRenderer))
            {
                this._renderer = this.GetFirstComponent<MeshRenderer>(this.transform);
            }

            return this._renderer;
        }
    }

    protected T GetFirstComponent<T>(Transform obj)
        where T : Component
    {
        T component;
        int how_many, i = 0;

        component = obj.GetComponent<T>();
        how_many = obj.childCount;

        // is there no component, but child objects to check?
        if (component == default(T) && 0 < how_many)
        {
            // check until we find a component
            for (; how_many > i && null == component; i += 1)
            {
                component = this.GetFirstComponent<T>(obj.GetChild(i));
            }
        }

        return component;
    }
}
