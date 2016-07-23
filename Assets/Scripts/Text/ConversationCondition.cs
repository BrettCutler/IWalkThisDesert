using UnityEngine;
using System.Collections;
using WyrmTale;

public class ConversationCondition
{
  private string m_Property;
  private ConditionOperand m_Operand;
  private int m_Value;


  public void Deserialize( JSON json )
  {
    m_Property = json.ToString( "property" );
    m_Operand = (ConditionOperand)json.ToInt( "operand" );
    m_Value = json.ToInt( "value" );
  }

  /// <summary>
  /// Look
  /// </summary>
  /// <returns></returns>
  public bool Evaluate()
  {
    int existingValue = GameStateFacts.Instance.Lookup( m_Property );

    switch( m_Operand )
    {
      case ConditionOperand.EQ:
        return m_Value == existingValue;
      case ConditionOperand.GT:
        return m_Value > existingValue;
      case ConditionOperand.GTE:
        return m_Value >= existingValue;
      case ConditionOperand.LT:
        return m_Value < existingValue;
      case ConditionOperand.LTE:
        return m_Value <= existingValue;
    }

    return false;
  }
}

public enum ConditionOperand
{
  EQ,
  GT,
  GTE,
  LT,
  LTE
}
